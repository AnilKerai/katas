namespace MagicSquare;

public class MagicSquareCalculatorTests
{
    public static IEnumerable<object[]> WhenGivenIncorrectNumberOfValuesData(){
        yield return [new List<decimal>()];
        yield return [new List<decimal>{ 1.0m }];
    }
    
    [Theory]
    [MemberData(nameof(WhenGivenIncorrectNumberOfValuesData))]
    public void CalculateMagicNumber_WhenGivenIncorrectNumberOfValues_ThrowsArgumentException(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        Action action = () => magicSquareCalculator.CalculateMagicNumber(values);
        
        var ex = Should.Throw<ArgumentException>(action);
        ex.Message.ShouldContain("The number of values must be equal to the square of the grid size.");
    }
    
    public static IEnumerable<object[]> WhenGivenArrayOfAllZerosData(){
        yield return [new List<decimal>{ 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m, 0.0m }];
    }
    
    [Theory]
    [MemberData(nameof(WhenGivenArrayOfAllZerosData))]
    public void CalculateMagicNumber_WhenGivenArrayOfAllZeros_ReturnsMagicNumberZeroAndGridOfZeros(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);

        ShouldBeAMagicSquare(result, values, 0.0m);
        result.Grid!.Cast<decimal>().ShouldAllBe(cell => cell == 0.0m);
    }
    
    public static IEnumerable<object[]> TotalOfInputValuesIsNotDivisibleByGridSizeData(){
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 2.0m }];
    }
    
    [Theory]
    [MemberData(nameof(TotalOfInputValuesIsNotDivisibleByGridSizeData))]
    public void CalculateMagicNumber_TotalOfInputValuesIsNotDivisibleByGridSize_ReturnsNotAMagicSquare(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values.Select(v => (decimal)v).ToList());

        ShouldBeNotAMagicSquare(result);
    }
    
    public static IEnumerable<object[]> InputArrayDoesNotContainMDividedByGridSizeValueData(){
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 10.0m }];
    }
    
    [Theory]
    [MemberData(nameof(InputArrayDoesNotContainMDividedByGridSizeValueData))]
    public void CalculateMagicNumber_InputArrayDoesNotContainMDividedByGridSizeValue_ReturnsNotAMagicSquare(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);

        ShouldBeNotAMagicSquare(result);
    }
    
    public static IEnumerable<object[]> WhenGivenCorrectNumberOfValuesData(){
        yield return [
            new List<decimal>{ 1.0m, 1.5m, 2.0m, 2.5m, 3.0m, 3.5m, 4.0m, 4.5m, 5.0m },
            9.0m
        ];
        yield return [
            new List<decimal>{ 1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m, 9.0m },
            15.0m
        ];
    }
    
    [Theory]
    [MemberData(nameof(WhenGivenCorrectNumberOfValuesData))]
    public void CalculateMagicNumber_WhenGivenCorrectNumberOfValues_ReturnsCorrectNumber(
        List<decimal> values,
        decimal expectedMagicNumber)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);

        ShouldBeAMagicSquare(result, values, expectedMagicNumber);
    }

    public static IEnumerable<object[]> WhenAnArrangementExistsData(){
        // The kata's second example in a different order: the order of the input is not the layout of the grid.
        yield return [new List<decimal>{ 5.0m, 9.0m, 1.0m, 3.0m, 8.0m, 2.0m, 7.0m, 4.0m, 6.0m }, 15.0m];
        // Every value from 0 to 8 must be used exactly once.
        yield return [new List<decimal>{ 0.0m, 1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m }, 12.0m];
        // The center is 6, and the pairs around it are 1 + 11, 3 + 9, 4 + 8 and 5 + 7.
        yield return [new List<decimal>{ 1.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m, 9.0m, 11.0m }, 18.0m];
        // The center value (5) appears three times: two of them sit opposite each other, with three 4 + 6 pairs.
        yield return [new List<decimal>{ 4.0m, 4.0m, 4.0m, 5.0m, 5.0m, 5.0m, 6.0m, 6.0m, 6.0m }, 15.0m];
        // Every value is the center value.
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m }, 3.0m];
        // The total (4.5) is not a whole multiple of 3, so the magic number is 1.5.
        yield return [new List<decimal>{ -3.5m, -2.5m, -1.5m, -0.5m, 0.5m, 1.5m, 2.5m, 3.5m, 4.5m }, 1.5m];
    }

    [Theory]
    [MemberData(nameof(WhenAnArrangementExistsData))]
    public void CalculateMagicNumber_WhenAnArrangementExists_ReturnsMagicNumberAndValidGrid(
        List<decimal> values,
        decimal expectedMagicNumber)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);

        ShouldBeAMagicSquare(result, values, expectedMagicNumber);
    }

    public static IEnumerable<object[]> WhenNoArrangementExistsData(){
        // Every value must sit opposite one that sums with it to twice the center (here 5, so 10 needs a 0).
        yield return [new List<decimal>{ 1.0m, 1.0m, 2.0m, 4.0m, 5.0m, 6.0m, 7.0m, 9.0m, 10.0m }];
        // There are two 8s but only one 2 to sit opposite them.
        yield return [new List<decimal>{ 8.0m, 8.0m, 7.0m, 6.0m, 5.0m, 4.0m, 4.0m, 2.0m, 1.0m }];
        // The values pair up around the center (1 + 1 three times, and 2 + 0), but no layout of them makes every
        // row, column and diagonal sum to 3.
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 2.0m, 0.0m }];
        // The values pair up around the center (5) as 0 + 10, 1 + 9, 2 + 8 and 3 + 7, but no layout of the pairs
        // makes every line sum to 15.
        yield return [new List<decimal>{ 0.0m, 1.0m, 2.0m, 3.0m, 5.0m, 7.0m, 8.0m, 9.0m, 10.0m }];
        // The values pair up around the center (5) as four 1 + 9, but no layout of them makes every line sum to 15.
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 5.0m, 9.0m, 9.0m, 9.0m, 9.0m }];
    }

    [Theory]
    [MemberData(nameof(WhenNoArrangementExistsData))]
    public void CalculateMagicNumber_WhenNoArrangementExists_ReturnsNotAMagicSquare(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);

        ShouldBeNotAMagicSquare(result);
    }

    // A success result holds a 3x3 grid that uses each input value once and whose rows, columns and diagonals all
    // sum to the magic number.
    private static void ShouldBeAMagicSquare(
        MagicSquareResult result,
        IReadOnlyList<decimal> values,
        decimal expectedMagicNumber)
    {
        result.Reason.ShouldBe("The given number is a magic square");
        result.MagicNumber.ShouldBe(expectedMagicNumber);

        var grid = result.Grid.ShouldNotBeNull();
        grid.GetLength(0).ShouldBe(3);
        grid.GetLength(1).ShouldBe(3);
        grid.Cast<decimal>().OrderBy(cell => cell).ShouldBe(values.OrderBy(value => value));

        var lineSums = new List<decimal>();
        for (var i = 0; i < 3; i++)
        {
            lineSums.Add(grid[i, 0] + grid[i, 1] + grid[i, 2]);
            lineSums.Add(grid[0, i] + grid[1, i] + grid[2, i]);
        }
        lineSums.Add(grid[0, 0] + grid[1, 1] + grid[2, 2]);
        lineSums.Add(grid[0, 2] + grid[1, 1] + grid[2, 0]);

        lineSums.ShouldBe(Enumerable.Repeat(expectedMagicNumber, 8));
    }

    private static void ShouldBeNotAMagicSquare(MagicSquareResult result)
    {
        result.Reason.ShouldBe("The given number is not a magic square");
        result.MagicNumber.ShouldBeNull();
        result.Grid.ShouldBeNull();
    }
}