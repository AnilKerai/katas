# Kata: FizzBuzz

A .NET 10 console app plus an xUnit test project, built with TDD (see the commit history for each
red / green / refactor step).

## Problem Statement

For each whole number from 1 upwards:

- If it is divisible by 15, return `FizzBuzz`
- Else if it is divisible by 3, return `Fizz`
- Else if it is divisible by 5, return `Buzz`
- Otherwise return the number itself as a string

The API is two static methods on `FizzBuzzer`:

- `Convert(int n)` returns the FizzBuzz value for a single number
- `Sequence(int from, int to)` returns the values for the inclusive range `from..to`

## Validation

Only positive numbers are valid. Anything else throws `ArgumentOutOfRangeException`:

- `Convert(n)` when `n <= 0`
- `Sequence(from, to)` when `from <= 0` or `to < from`

`Sequence` validates eagerly: it throws when it is called, not when the result is enumerated.

## Commands

Run from the `FizzBuzz/` folder:

```
dotnet build
dotnet test
dotnet run --project src/FizzBuzz
dotnet run --project src/FizzBuzz -- 15
```

## Console app arguments and exit codes

The app prints one value per line to stdout for the range `1..upperBound`.

| Arguments                      | Behaviour                                    | Exit code |
|--------------------------------|----------------------------------------------|-----------|
| none                           | prints 1 to 100                              | 0         |
| `<upperBound>` (int, `>= 1`)   | prints 1 to `upperBound`; extra args ignored | 0         |
| first arg not an int, or `< 1` | prints the usage message to stderr           | 1         |

Usage message: `Usage: FizzBuzz [upperBound]  (upperBound must be a positive integer)`

## Project layout

This kata is the exception to the repo's single-project-per-kata convention. It has separate
`src/` and `tests/` projects and its own `FizzBuzz.sln`. Its `Directory.Build.props` deliberately
does not inherit the root props (which would inject the test stack into the console app and use a
relative `Katas.Testing` path that is wrong for nested projects). The two projects are also
registered in the root `Katas.sln`.
