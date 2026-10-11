# Kata: Roman Numerals

Convert a whole number to its Roman numeral, built with TDD (see the commit history for each red / green /
refactor step).

## Problem Statement

Roman numerals are written with seven symbols:

| Symbol | Value |
|--------|-------|
| `I`    | 1     |
| `V`    | 5     |
| `X`    | 10    |
| `L`    | 50    |
| `C`    | 100   |
| `D`    | 500   |
| `M`    | 1000  |

Symbols are written largest first and added together: `VI` is 6 and `MMXXVI` is 2026.

A symbol is never written four times in a row. Instead, a smaller symbol placed before a larger one is
subtracted from it. There are six of these pairs:

| Pair | Value |
|------|-------|
| `IV` | 4     |
| `IX` | 9     |
| `XL` | 40    |
| `XC` | 90    |
| `CD` | 400   |
| `CM` | 900   |

The API is one static method on `RomanNumeral`:

- `From(int number)` returns the Roman numeral for `number`

## Examples

| Number | Roman numeral |
|--------|---------------|
| 1      | `I`           |
| 3      | `III`         |
| 4      | `IV`          |
| 9      | `IX`          |
| 14     | `XIV`         |
| 40     | `XL`          |
| 1994   | `MCMXCIV`     |
| 2026   | `MMXXVI`      |
| 3999   | `MMMCMXCIX`   |

## Validation

Only 1 to 3999 can be written this way: there is no symbol for zero, and 4000 would need four `M`s. Any other
number throws `ArgumentOutOfRangeException`.

## Commands

Run from the repo root:

```
dotnet test RomanNumerals/RomanNumerals.csproj
```
