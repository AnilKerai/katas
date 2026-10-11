# CLAUDE.md

A collection of code katas (TDD training exercises) written in C# / .NET, grouped under a single
solution (`Katas.sln`).

## Structure

Each kata is its own project directory at the repo root, referenced from `Katas.sln`:

- `ABCGame/`
- `BowlingScoreCalculator/`
- `FizzBuzz/`
- `MagicSquare/`
- `PrintDiamond/`
- `Reordering/`
- `TextFormatter/`

`Katas.Testing/` is a shared support project (not a kata) referenced automatically by every kata.

A typical kata directory contains:

- `README.md` — the problem statement / kata brief
- `<Name>.cs` — the solution (production code)
- `<Name>Tests.cs` — the xUnit tests
- `<Name>.csproj` — the project file

FizzBuzz is also a console app (`OutputType` `Exe`, `GenerateProgramFile=false`, so its own `Program.cs` is
the entry point), run with `dotnet run --project FizzBuzz`.

## Languages & test frameworks

- **Language / TFM:** C#, `net10.0` for every project, with `Nullable` and `ImplicitUsings` enabled. The
  TFM is set once in `Directory.Build.props`; project files must not set `TargetFramework`.
- **Test framework:** xUnit v2 (the `xunit` package), run through VSTest via `dotnet test`. Each kata
  project is itself a test project — there is no separate src/test split; the solution code and its
  tests live side by side in the same project.
- **Assertions:** Shouldly.
- **Test data / mocking:** AutoFixture (incl. `AutoFixture.Xunit2`) and NSubstitute.

Versions are pinned centrally via `Directory.Packages.props` (central package management is on, so
`<PackageReference>` entries omit versions).

## How test dependencies are wired

`Directory.Build.props` injects the common test stack into every kata project automatically (for any
project whose name is not `Katas.Testing`):

- The test packages above (`Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`,
  `coverlet.collector`, `Shouldly`, `AutoFixture`, `AutoFixture.AutoNSubstitute`, `AutoFixture.Idioms`,
  `AutoFixture.Xunit2`, `NSubstitute`).
- Global `using`s: `Xunit`, `AutoFixture`, `AutoFixture.Xunit2`, `Shouldly`, `Katas.Testing`.
- A project reference to `Katas.Testing`.

`Katas.Testing/Attributes.cs` provides custom xUnit data attributes:
`AutoNSubstituteDataAttribute` and `InlineAutoNSubstituteDataAttribute`. `Katas.Testing.csproj` references
directly everything that file compiles against — `AutoFixture`, `AutoFixture.AutoNSubstitute`,
`AutoFixture.Xunit2`, `NSubstitute` and `xunit.extensibility.core` — so it builds against the same
versions as the katas rather than the lowest versions AutoFixture's dependency ranges allow. It does not
reference `xunit`: it is a library, not a test project.

This means a new kata project does **not** need to add its own package references, `Usings.cs`, or
test attributes — they come from the shared props/project.

## Build & test

Run from the repo root.

Build the whole solution:

```
dotnet build
```

Run all tests:

```
dotnet test
```

Run the tests for a single kata:

```
dotnet test MagicSquare/MagicSquare.csproj
```

## Conventions & gotchas

- **TDD is the point.** The katas are training exercises; READMEs (e.g. `MagicSquare/README.md`)
  explicitly call out using TDD. Drive changes from tests.
- **One project per kata**, registered in `Katas.sln`. Adding a new kata means a new directory + a
  new `.csproj` added to the solution; the shared test setup applies automatically.
- Kata projects are not packable (`IsPackable=false`); they are test projects, not libraries/apps.
- Don't add per-project package versions — central package management owns versions in
  `Directory.Packages.props`.
- Don't add per-project `Usings.cs`/`Attributes.cs`; `Directory.Build.props` removes them and global
  usings are provided.
- **Stay on xunit v2 and VSTest for now.** nuget.org marks `xunit` deprecated in favour of `xunit.v3`, but
  `xunit.v3` 4.x is not adopted: on the .NET 10 SDK its Microsoft.Testing.Platform targets make VSTest
  `dotnet test` fail unless the repo opts in to Microsoft Testing Platform (via `global.json`), and v3
  test projects are executables that the runner launches itself (first argument `@@`), which clashes
  with FizzBuzz's own `Program.cs`. Don't add a `global.json` or switch to Microsoft Testing Platform.
- **NSubstitute is held at 5.3.0.** `AutoFixture.AutoNSubstitute` 4.18.1 (the latest stable AutoFixture)
  requires NSubstitute `< 6.0.0`; a newer NSubstitute raises NU1608, which is an error in FizzBuzz
  (`TreatWarningsAsErrors`). Revisit when AutoFixture 5 ships a stable release.
