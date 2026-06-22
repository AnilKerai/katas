# CLAUDE.md

A collection of code katas (TDD training exercises) written in C# / .NET, grouped under a single
solution (`Katas.sln`).

## Structure

Each kata is its own project directory at the repo root, referenced from `Katas.sln`:

- `ABCGame/`
- `BowlingScoreCalculator/`
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

## Languages & test frameworks

- **Language / TFM:** C#, `net9.0`, with `Nullable` and `ImplicitUsings` enabled.
- **Test framework:** xUnit. Each kata project is itself a test project — there is no separate
  src/test split; the solution code and its tests live side by side in the same project.
- **Assertions:** Shouldly.
- **Test data / mocking:** AutoFixture (incl. `AutoFixture.Xunit2`) and NSubstitute.

Versions are pinned centrally via `Directory.Packages.props` (central package management is on, so
`<PackageReference>` entries omit versions).

## How test dependencies are wired

`Directory.Build.props` injects the common test stack into every kata project automatically (for any
project whose name is not `Katas.Testing`):

- The test packages above (`Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio`,
  `coverlet.collector`, Shouldly, AutoFixture*, NSubstitute).
- Global `using`s: `Xunit`, `AutoFixture`, `AutoFixture.Xunit2`, `Shouldly`, `Katas.Testing`.
- A project reference to `Katas.Testing`.

`Katas.Testing/Attributes.cs` provides custom xUnit data attributes:
`AutoNSubstituteDataAttribute` and `InlineAutoNSubstituteDataAttribute`.

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
