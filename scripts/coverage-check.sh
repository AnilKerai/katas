#!/usr/bin/env bash
# Coverage gate: every production file needs 100% line and >= 95% branch coverage.
#
# Runs the coverage command from CLAUDE.md into a fresh results directory, prints one row per
# production file and exits 1 when a file is under target and not listed in coverage-exceptions.txt.
# Exit codes: 0 all files on target, 1 a file is under target, 2 coverage could not be measured.
#
# A production file is any .cs file in a kata directory (a directory with a .csproj), excluding
# *Tests.cs, bin/, obj/ and the Katas.Testing / Katas.Testing.Tests projects. Parsing uses python3
# (standard library only), so there is nothing to install.
set -euo pipefail

cd "$(dirname "${BASH_SOURCE[0]}")/.."

results_dir=TestResults/coverage-check
rm -rf "$results_dir"
mkdir -p "$results_dir"

echo "Running tests with coverage (log: $results_dir/dotnet-test.log)..."
if ! dotnet test --coverage --coverage-output-format cobertura --coverage-settings CodeCoverage.config \
    --results-directory "$results_dir" >"$results_dir/dotnet-test.log" 2>&1; then
    tail -n 40 "$results_dir/dotnet-test.log" >&2
    echo "coverage-check: dotnet test failed, so coverage was not checked." >&2
    exit 2
fi

python3 - "$results_dir" scripts/coverage-exceptions.txt <<'PYTHON'
import os
import sys
import xml.etree.ElementTree as ET
from collections import defaultdict

LINE_TARGET = 100    # percent
BRANCH_TARGET = 95   # percent
NOT_KATAS = {"Katas.Testing", "Katas.Testing.Tests"}

results_dir, exceptions_path = sys.argv[1:3]
root = os.path.realpath(".")


def fail(message):
    """Coverage could not be measured (as opposed to measured and found short): exit code 2."""
    print(f"coverage-check: {message}", file=sys.stderr)
    sys.exit(2)


def production_files():
    """Repo-relative paths of every production .cs file (decision D1)."""
    for kata in sorted(os.listdir(root)):
        kata_dir = os.path.join(root, kata)
        if kata in NOT_KATAS or not os.path.isdir(kata_dir):
            continue
        if not any(name.endswith(".csproj") for name in os.listdir(kata_dir)):
            continue
        for folder, subfolders, names in os.walk(kata_dir):
            subfolders[:] = sorted(d for d in subfolders if d not in ("bin", "obj"))
            for name in sorted(names):
                if name.endswith(".cs") and not name.endswith("Tests.cs"):
                    yield os.path.relpath(os.path.join(folder, name), root)


def load_exceptions(files):
    """Reads `path | reason` (whole file) and `path::Member | reason` lines; '#' starts a comment."""
    file_exceptions, member_exceptions = {}, {}
    with open(exceptions_path, encoding="utf-8") as handle:
        for number, raw in enumerate(handle, 1):
            text = raw.strip()
            if not text or text.startswith("#"):
                continue
            target, _, reason = (part.strip() for part in text.partition("|"))
            path, _, member = target.partition("::")
            if not path or not reason:
                fail(f"{exceptions_path}:{number}: expected `path[::Member] | reason`")
            if path not in files:
                fail(f"{exceptions_path}:{number}: {path} is not a production file")
            if member:
                member_exceptions[(path, member)] = reason
            else:
                file_exceptions[path] = reason
    return file_exceptions, member_exceptions


def read_coverage(member_exceptions):
    """Merges every cobertura report into {file: [covered lines, lines, covered branches, branches]}.

    A line counts as covered when any method that owns it ran it (instance field initialisers, for
    example, are repeated in every constructor). Branches are counted once per method and line.
    Methods named in member_exceptions are left out. Also returns the (file, member) pairs it saw.
    """
    reports = [os.path.join(results_dir, f) for f in sorted(os.listdir(results_dir)) if f.endswith(".cobertura.xml")]
    if not reports:
        fail(f"no cobertura reports in {results_dir}")

    line_hits = defaultdict(int)   # (file, line number) -> highest hit count
    branches = {}                  # (file, class, method, signature, line number) -> (covered, total)
    members = set()
    for report in reports:
        for cls in ET.parse(report).getroot().iter("class"):
            path = os.path.relpath(os.path.realpath(cls.get("filename")), root)
            for method in cls.iter("method"):
                members.add((path, method.get("name")))
                if (path, method.get("name")) in member_exceptions:
                    continue
                for line in method.iter("line"):
                    number = int(line.get("number"))
                    line_hits[path, number] = max(line_hits[path, number], int(line.get("hits")))
                    if line.get("branch") == "True":
                        # condition-coverage looks like "50% (2/4)"
                        covered, total = line.get("condition-coverage").split("(")[1].rstrip(")").split("/")
                        key = (path, cls.get("name"), method.get("name"), method.get("signature"), number)
                        branches[key] = (max(int(covered), branches.get(key, (0, 0))[0]), int(total))

    coverage = defaultdict(lambda: [0, 0, 0, 0])
    for (path, _), hits in line_hits.items():
        coverage[path][0] += hits > 0
        coverage[path][1] += 1
    for (path, *_), (covered, total) in branches.items():
        coverage[path][2] += covered
        coverage[path][3] += total
    return coverage, members


def percent(covered, total):
    return 100.0 if total == 0 else 100.0 * covered / total


files = list(production_files())
file_exceptions, member_exceptions = load_exceptions(files)
coverage, members = read_coverage(member_exceptions)
for path, member in member_exceptions:
    if (path, member) not in members:
        fail(f"{exceptions_path} names {path}::{member}, which is not in the coverage report")

width = max(len(path) for path in files + ["File"])
print(f"\n{'File':<{width}} {'Lines':>14} {'Branches':>16}")
failures = []
for path in files:
    covered_lines, total_lines, covered_branches, total_branches = coverage.get(path, (0, 0, 0, 0))
    problems = []
    if path not in coverage:
        problems.append("not in the coverage report")
    # Integer arithmetic, so 94.99% can never round up to a pass.
    if covered_lines * 100 < total_lines * LINE_TARGET:
        problems.append(f"lines below {LINE_TARGET}%")
    if covered_branches * 100 < total_branches * BRANCH_TARGET:
        problems.append(f"branches below {BRANCH_TARGET}%")

    if not problems:
        note = "ok" if path not in file_exceptions else "ok (its exception is no longer needed, remove it)"
    elif path in file_exceptions:
        note = f"EXCEPTION ({file_exceptions[path]})"
    else:
        note = "FAIL: " + ", ".join(problems)
        failures.append(path)
    print(f"{path:<{width}} {covered_lines:>5}/{total_lines:<5} {percent(covered_lines, total_lines):>5.1f}%"
          f" {covered_branches:>5}/{total_branches:<5} {percent(covered_branches, total_branches):>5.1f}%  {note}")

for (path, member), reason in sorted(member_exceptions.items()):
    print(f"  excluded member {path}::{member} ({reason})")

if failures:
    print(f"\ncoverage-check: FAILED, under target: {', '.join(failures)}")
    sys.exit(1)
print(f"\ncoverage-check: ok, {len(files)} production files (lines {LINE_TARGET}%, branches >= {BRANCH_TARGET}%)")
PYTHON
