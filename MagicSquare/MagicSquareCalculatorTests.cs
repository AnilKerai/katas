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
    public void CalculateMagicNumber_WhenGivenArrayOfAllZeros_ReturnsEmptyResult(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);
        
        result.MagicNumber.ShouldBeNull();
        result.Grid.ShouldBeNull();
    }
    
    public static IEnumerable<object[]> TotalOfInputValuesIsNotDivisibleByGridSizeData(){
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 2.0m }];
    }
    
    [Theory]
    [MemberData(nameof(TotalOfInputValuesIsNotDivisibleByGridSizeData))]
    public void CalculateMagicNumber_TotalOfInputValuesIsNotDivisibleByGridSize_ThrowsException(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        Action action = () => magicSquareCalculator.CalculateMagicNumber(values.Select(v => (decimal)v).ToList());
        
        var ex = action.ShouldThrow<InvalidOperationException>();
        ex.Message.ShouldContain("The total of the input values is not divisible by the grid size.");
    }
    
    public static IEnumerable<object[]> InputArrayDoesNotContainMDividedByGridSizeValueData(){
        yield return [new List<decimal>{ 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 1.0m, 10.0m }];
    }
    
    [Theory]
    [MemberData(nameof(InputArrayDoesNotContainMDividedByGridSizeValueData))]
    public void CalculateMagicNumber_InputArrayDoesNotContainMDividedByGridSizeValue_ThrowsException(
        List<decimal> values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        Action action = () => magicSquareCalculator.CalculateMagicNumber(values);
        
        var ex = action.ShouldThrow<InvalidOperationException>();
        ex.Message.ShouldContain("The input array does not contain the value M / GridSize");
    }
    
    public static IEnumerable<object[]> WhenGivenCorrectNumberOfValuesData(){
        yield return [
            new List<decimal>{ 1.0m, 1.5m, 2.0m, 2.5m, 3.0m, 3.5m, 4.0m, 4.5m, 5.0m },
            9.0m,
            new [] { 4.5m, 1.0m, 3.5m },
            new [] { 2.0m, 3.0m, 4.0m },
            new [] { 2.5m, 5.0m, 1.5m }
        ];
        yield return [
            new List<decimal>{ 1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m, 9.0m },
            15.0m,
            new [] { 8.0m, 1.0m, 6.0m },
            new [] { 3.0m, 5.0m, 7.0m },
            new [] { 4.0m, 9.0m, 2.0m }
        ];
    }
    
    [Theory]
    [MemberData(nameof(WhenGivenCorrectNumberOfValuesData))]
    public void CalculateMagicNumber_WhenGivenCorrectNumberOfValues_ReturnsCorrectNumber(
        List<decimal> values,
        decimal expectedMagicNumber,
        decimal[] expectedFirstRow,
        decimal[] expectedSecondRow,
        decimal[] expectedThirdRow)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);
        
        result.MagicNumber.ShouldNotBeNull();
        result.MagicNumber.ShouldBe(expectedMagicNumber);
        result.Grid.ShouldNotBeNull();

        var expectedGrid = new List<decimal[]>
        {
            expectedFirstRow,
            expectedSecondRow,
            expectedThirdRow
        };

        for (var i = 0; i < result.Grid.GetLength(0); i++)
        {
            for (var j = 0; j < result.Grid.GetLength(1); j++)
            {
                ((decimal) result.Grid.GetValue(i, j)!).ShouldBe(expectedGrid[i][j]);
            }
        }
    }
}