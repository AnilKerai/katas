namespace MagicSquare;

public class MagicSquareCalculator
{
    private const int GridSize = 3;
    private static int ExpectedNumberOfPairs => (GridSize * GridSize - 1) / 2;
    
    public MagicSquareResult CalculateMagicNumber(IReadOnlyList<decimal> values)
    {
        EnsureCorrectCount(values);
        
        if (IsAllZeros(values))
            return MagicSquareResult.Empty;

        var (magicNumber, centerNumber) = ComputeMagicAndCenter(values);

        EnsureCenterExists(values, centerNumber);

        var expectedSumOfRemainingPairs = magicNumber - centerNumber;
        var remainingNumbers = BuildRemainingNumbers(values, centerNumber);

        var pairs = FindPairs(remainingNumbers, expectedSumOfRemainingPairs);
        if (pairs is null || pairs.Count != ExpectedNumberOfPairs)
            return new MagicSquareResult { Reason = "The given number is not a magic square" };

        var solution = CalculateGrid(pairs, centerNumber, magicNumber);
        
        return new MagicSquareResult
        {
            Grid = solution,
            MagicNumber = magicNumber,
            Reason = "The given number is a magic square"
        };
    }

    private decimal[,] CalculateGrid(IReadOnlyList<decimal[]> pairs, decimal centerNumber, decimal magicNumber)
    {
        var result = new decimal[GridSize, GridSize];
        
        // Set center
        result[1, 1] = centerNumber;
        
        // Initiate loop
        foreach (var pair in pairs)
        {
            var secondCornerFound = false;
            
            result[0, 0] = pair[0]; // Set Top Left
            result[2, 2] = pair[1]; // Set Bottom Right

            foreach (var secondPair in pairs)
            {
                result[0, 2] = secondPair[0]; // Set Top Right
                result[2, 0] = secondPair[1]; // Set Bottom Left
                
                // Sum top row
                var topRowSum = result[0, 0] + result[0, 2];

                if (topRowSum < magicNumber)
                {
                    var middleNumberInRow = magicNumber - result[0, 0] - result[0, 2];
                
                    var middleNumberExistsInArray = pairs.Any(p => p.Contains(middleNumberInRow));

                    if (middleNumberExistsInArray)
                    {
                        secondCornerFound = true;
                        continue;
                    }
                }
                
                // Backtrack second pair and try next pair
                result[0, 2] = 0;
                result[2, 0] = 0;
            }

            if (!secondCornerFound)
            {
                // Backtrack first pair and try next pair
                result[0, 0] = 0;
                result[2, 2] = 0;
            }
            else
            {
                // Fill remaining pairs
                result[0, 1] = magicNumber - result[0, 0] - result[0, 2];
                result[1, 0] = magicNumber - result[0, 0] - result[2, 0];
                result[1, 2] = magicNumber - result[0, 2] - result[2, 2];
                result[2, 1] = magicNumber - result[2, 0] - result[2, 2];
                
                // Check if all rows and columns sum to magic number
                var topRowSum = result[0, 0] + result[0, 1] + result[0, 2];
                var middleRowSum = result[1, 0] + result[1, 1] + result[1, 2];
                var bottomRowSum = result[2, 0] + result[2, 1] + result[2, 2];
                var leftColumnSum = result[0, 0] + result[1, 0] + result[2, 0];
                var middleColumnSum = result[0, 1] + result[1, 1] + result[2, 1];
                var rightColumnSum = result[0, 2] + result[1, 2] + result[2, 2];
                var topLeftDiagonalSum = result[0, 0] + result[1, 1] + result[2, 2];
                var topRightDiagonalSum = result[0, 2] + result[1, 1] + result[2, 0];

                if (topRowSum == magicNumber &&
                    middleRowSum == magicNumber &&
                    bottomRowSum == magicNumber &&
                    leftColumnSum == magicNumber &&
                    middleColumnSum == magicNumber &&
                    rightColumnSum == magicNumber &&
                    topLeftDiagonalSum == magicNumber &&
                    topRightDiagonalSum == magicNumber)
                {
                    // Found solution
                    break;
                }
            }
        }
        
        return result;
    }

    private static void EnsureCorrectCount(IReadOnlyList<decimal> values)
    {
        if (values.Count != GridSize * GridSize)
            throw new ArgumentException("The number of values must be equal to the square of the grid size.");
    }

    private static bool IsAllZeros(IReadOnlyList<decimal> values) => values.All(v => v == 0);

    private static (decimal magicNumber, decimal centerNumber) ComputeMagicAndCenter(IReadOnlyList<decimal> values)
    {
        var total = values.Sum();
        if (total % GridSize != 0)
            throw new InvalidOperationException("The total of the input values is not divisible by the grid size.");

        var magicNumber = total / GridSize;
        var centerNumber = magicNumber / GridSize;
        return (magicNumber, centerNumber);
    }

    private static void EnsureCenterExists(IReadOnlyList<decimal> values, decimal centerNumber)
    {
        if (!values.Contains(centerNumber))
            throw new InvalidOperationException("The input array does not contain the value M / GridSize");
    }

    private static decimal[] BuildRemainingNumbers(IReadOnlyList<decimal> values, decimal centerNumber)
    {
        return values
            .Where(v => v != centerNumber)
            .OrderByDescending(v => v)
            .ToArray();
    }

    private static List<decimal[]>? FindPairs(decimal[] remainingNumbers, decimal expectedSumOfRemainingPairs)
    {
        var counts = remainingNumbers
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());

        var ordered = counts.Keys.OrderByDescending(v => v).ToArray();
        var pairs = new List<decimal[]>();

        foreach (var x in ordered)
        {
            while (counts.TryGetValue(x, out var cx) && cx > 0)
            {
                var y = expectedSumOfRemainingPairs - x;
                if (!counts.TryGetValue(y, out var cy))
                    return null;

                if (x == y)
                {
                    if (cy < 2) return null;
                    counts[x] = cx - 2;
                }
                else
                {
                    if (cy < 1) return null;
                    counts[x] = cx - 1;
                    counts[y] = cy - 1;
                }

                pairs.Add([x, y]);
            }
        }

        return pairs.Count == ExpectedNumberOfPairs ? pairs : null;
    }
}

public sealed record MagicSquareResult
{
    public decimal[,]? Grid { get; init; }
    public decimal? MagicNumber { get; init; }
    public string? Reason { get; init; }
    public static MagicSquareResult Empty => new();
}
