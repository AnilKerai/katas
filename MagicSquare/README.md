# Kata: 3×3 Magic Square

## Problem Statement
You are given exactly 9 numbers.
Your task is to determine whether these numbers can be arranged into a 3 × 3 grid such that:

- The sum of each row is the same 
- The sum of each column is the same 
- The sum of both diagonals is the same

This shared value is called the **Magic Number**.

If a valid arrangement exists, return:

- The Magic Number 
- The final 3 × 3 grid, represented as an array of arrays

If no valid arrangement exists, return an appropriate failure result.

## Input Rules
- The input will always be an array of 9 numbers 
- Numbers may be integers or decimals 
- Each number must be used exactly once 
- The order of the input array does not represent the grid layout

## Output Format

Magic Number: <number>

Grid:
```csharp
[
  [a, b, c],
  [d, e, f],
  [g, h, i]
]
```

## Example Inputs
### Example 1
#### Input
```json
[1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0]
```

Expected Magic Number:
`9.0`

One Valid Output Grid:
```csharp
[
    [4.5, 1.0, 3.5],
    [2.0, 3.0, 4.0],
    [2.5, 5.0, 1.5]
]
```

### Example 2
#### Input:
```json
[1, 2, 3, 4, 5, 6, 7, 8, 9]
```

Expected Magic Number:
`15`

One Valid Output Grid:
```csharp
[
    [8, 1, 6],
    [3, 5, 7],
    [4, 9, 2]
]
```

## Notes
- There may be multiple valid grids
  - Returning any one correct solution is acceptable
- Focus on correctness first; performance is secondary
- You may assume a fixed 3 × 3 grid for this kata
- Use TDD