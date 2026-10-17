namespace MagicSquare;

public class MagicSquareCalculator
{
    private const int GridSize = 3;

    // The cells around the center, in pairs that sit opposite each other through it.
    private static readonly (int Row, int Column)[] OuterCells =
    [
        (0, 0), (2, 2),
        (0, 2), (2, 0),
        (0, 1), (2, 1),
        (1, 0), (1, 2)
    ];

    public MagicSquareResult CalculateMagicNumber(IReadOnlyList<decimal> values)
    {
        EnsureCorrectCount(values);

        var magicNumber = values.Sum() / GridSize;
        var grid = FindGrid(values, magicNumber);
        if (grid is null)
            return new MagicSquareResult { Reason = "The given number is not a magic square" };

        return new MagicSquareResult
        {
            Grid = grid,
            MagicNumber = magicNumber,
            Reason = "The given number is a magic square"
        };
    }

    private static void EnsureCorrectCount(IReadOnlyList<decimal> values)
    {
        if (values.Count != GridSize * GridSize)
            throw new ArgumentException("The number of values must be equal to the square of the grid size.");
    }

    private static decimal[,]? FindGrid(IReadOnlyList<decimal> values, decimal magicNumber)
    {
        // The center of a magic square is a third of its magic number, so it must be one of the input values.
        var centerNumber = magicNumber / GridSize;
        var outerNumbers = values.ToList();
        if (!outerNumbers.Remove(centerNumber))
            return null;

        var grid = new decimal[GridSize, GridSize];
        grid[1, 1] = centerNumber;

        return PlaceOuterNumbers(outerNumbers, 0, grid, magicNumber) ? grid : null;
    }

    // Tries every order of the numbers around the center, one cell at a time. A number is swapped into the next
    // cell, and the search carries on only while the cells placed so far can still be part of a magic square.
    private static bool PlaceOuterNumbers(List<decimal> outerNumbers, int position, decimal[,] grid, decimal magicNumber)
    {
        if (position == OuterCells.Length)
            return IsMagicSquare(grid, magicNumber);

        var (row, column) = OuterCells[position];
        for (var candidate = position; candidate < outerNumbers.Count; candidate++)
        {
            (outerNumbers[position], outerNumbers[candidate]) = (outerNumbers[candidate], outerNumbers[position]);
            grid[row, column] = outerNumbers[position];

            if (IsBalancedAroundCenter(grid, position) &&
                PlaceOuterNumbers(outerNumbers, position + 1, grid, magicNumber))
                return true;

            (outerNumbers[position], outerNumbers[candidate]) = (outerNumbers[candidate], outerNumbers[position]);
        }

        return false;
    }

    // Two cells opposite each other through the center sum to twice the center. The second cell of each pair is
    // the one that completes it, so only that one can be checked.
    private static bool IsBalancedAroundCenter(decimal[,] grid, int position)
    {
        if (position % 2 == 0)
            return true;

        var (row, column) = OuterCells[position];
        var (oppositeRow, oppositeColumn) = OuterCells[position - 1];
        return grid[row, column] + grid[oppositeRow, oppositeColumn] == 2 * grid[1, 1];
    }

    private static bool IsMagicSquare(decimal[,] grid, decimal magicNumber)
    {
        decimal[] lineSums =
        [
            grid[0, 0] + grid[0, 1] + grid[0, 2],
            grid[1, 0] + grid[1, 1] + grid[1, 2],
            grid[2, 0] + grid[2, 1] + grid[2, 2],
            grid[0, 0] + grid[1, 0] + grid[2, 0],
            grid[0, 1] + grid[1, 1] + grid[2, 1],
            grid[0, 2] + grid[1, 2] + grid[2, 2],
            grid[0, 0] + grid[1, 1] + grid[2, 2],
            grid[0, 2] + grid[1, 1] + grid[2, 0]
        ];

        return lineSums.All(sum => sum == magicNumber);
    }
}

public sealed record MagicSquareResult
{
    public decimal[,]? Grid { get; init; }
    public decimal? MagicNumber { get; init; }
    public string? Reason { get; init; }
}
