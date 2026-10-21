# Kata: Mars Rover

A rover drives around a wrapping 10x10 grid, built with TDD (see the commit history for each red / green /
refactor step).

## Problem Statement

A rover moves on a 10x10 grid. Coordinates run 0-9 on both axes; north is +y and east is +x.

The rover starts at `0:0:N`.

The API is one method on `Rover`:

- `Execute(string commands)` runs the commands in order and returns the final position as `x:y:D`,
  where `D` is one of `N`, `E`, `S`, `W`

## Commands

| Command | Meaning                                        |
|---------|------------------------------------------------|
| `L`     | Turn left 90 degrees                            |
| `R`     | Turn right 90 degrees                           |
| `M`     | Move one square forward in the current direction |

## Rules

1. The rover starts at `0:0:N`.
2. `Execute` returns the final position as `x:y:D`.
3. Commands are `L`, `R` and `M` as above.
4. The grid wraps: moving north from `y = 9` lands on `y = 0`, moving west from `x = 0` lands on `x = 9`,
   and so on for every edge.
5. A rover may be created with a set of obstacle coordinates. If the next `M` would enter an obstacle,
   the rover stays where it is, ignores the remaining commands, and returns `O:x:y:D` (its last position
   and direction).
6. An empty command string returns the current position. `null` throws `ArgumentNullException`.
   Any character other than `L`, `R`, `M` throws `ArgumentException` naming the character, and the rover
   does not move at all (commands are validated before any movement).

## Examples

| Commands        | Obstacles | Result      |
|-----------------|-----------|-------------|
| `MMRMMLM`      | none      | `2:3:N`    |
| `MMMMMMMMMM`   | none      | `0:0:N`    |
| `LM`            | none      | `9:0:W`    |
| `RRM`           | none      | `0:9:S`    |
| `MMMM`          | `(0, 3)`  | `O:0:2:N`  |
| `""`            | none      | `0:0:N`    |

## Commands

Run from the repo root:

```
dotnet test MarsRover/MarsRover.csproj
```