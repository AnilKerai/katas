namespace MagicSquare;

public class MagicSquareCalculator
{
    private const int GridSize = 3;
    private const int ExpectedNumberOfPairs = 4;
    
    public MagicSquareResult CalculateMagicNumber(double[] values)
    {
        if (values.Length != GridSize * GridSize)
            throw new ArgumentException("The number of values must be equal to the square of the grid size.");
        
        if (values.All(v => v == 0))
            return MagicSquareResult.Empty;

        var total = values.Sum();
        
        if (total % GridSize != 0)
            throw new InvalidOperationException("The total of the input values is not divisible by the grid size.");
        
        var magicNumber = total / GridSize;
        var centerNumber = magicNumber / GridSize;
        
        if (!values.Contains(centerNumber))
            throw new InvalidOperationException("The input array does not contain the value M / GridSize");

        var pairs = new List<double[]>();
        var expectedSumOfRemainingPairs = magicNumber - centerNumber;
        var remainingNumbers = 
            values
                .Where(v => v != centerNumber)
                .OrderByDescending(v => v)
                .ToArray();

        for (var i = 0; i < ExpectedNumberOfPairs; i++)
        {
            var pairFound = false;
            
            for (var j = i + 1; j < remainingNumbers.Length; j++)
            {
                if (remainingNumbers[i] + remainingNumbers[j] != expectedSumOfRemainingPairs) continue;
                
                pairs.Add([remainingNumbers[i], remainingNumbers[j]]);
                pairFound = true;
                break;
            }
            
            if (!pairFound)
                return new MagicSquareResult { Reason = "The given number is not a magic square" };
        }
        
        if (pairs.Count != ExpectedNumberOfPairs)
            return new MagicSquareResult { Reason = "The given number is not a magic square" };

        var solution = CalculateGrid(pairs, centerNumber, magicNumber);
        
        return new MagicSquareResult
        {
            Grid = solution,
            MagicNumber = magicNumber,
            Reason = "The given number is a magic square"
        };
    }

    private double[,] CalculateGrid(List<double[]> pairs, double centerNumber, double magicNumber)
    {
        var result = new double[GridSize, GridSize];
        
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
}

public class MagicSquareResult
{
    public double[,]? Grid { get; set; }
    public double? MagicNumber { get; set; }
    public string? Reason { get; set; }
    public static MagicSquareResult Empty => new();
}
