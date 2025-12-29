namespace MagicSquare;

public class MagicSquareCalculatorTests
{
    [Theory]
    [InlineData(new double[] { })]
    [InlineData(new[] { 1.0 })]
    public void CalculateMagicNumber_WhenGivenIncorrectNumberOfValues_ThrowsArgumentException(
        double[] values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        Action action = () => magicSquareCalculator.CalculateMagicNumber(values);
        
        var ex = Should.Throw<ArgumentException>(action);
        ex.Message.ShouldContain("The number of values must be equal to the square of the grid size.");
    }
    
    [Theory]
    [InlineData(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 })]
    public void CalculateMagicNumber_WhenGivenArrayOfAllZeros_ReturnsEmptyResult(
        double[] values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);
        
        result.MagicNumber.ShouldBeNull();
        result.Grid.ShouldBeNull();
    }
    
    [Theory]
    [InlineData(new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0})]
    public void CalculateMagicNumber_TotalOfInputValuesIsNotDivisbleByGridSize_ThrowsException(
        double[] values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        Action action = () => magicSquareCalculator.CalculateMagicNumber(values);
        
        var ex = action.ShouldThrow<InvalidOperationException>();
        ex.Message.ShouldContain("The total of the input values is not divisible by the grid size.");
    }
    
    [Theory]
    [InlineData(new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 10.0})]
    public void CalculateMagicNumber_InputArrayDoesNotContainMDividedByGridSizeValue_ThrowsException(
        double[] values)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        Action action = () => magicSquareCalculator.CalculateMagicNumber(values);
        
        var ex = action.ShouldThrow<InvalidOperationException>();
        ex.Message.ShouldContain("The input array does not contain the value M / GridSize");
    }
    
    [Theory]
    [InlineData(
        new[] { 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0 }, 
        9.0, 
        new[] { 4.5, 1.0, 3.5 },
        new[] { 2.0, 3.0, 4.0 },
        new[] { 2.5, 5.0, 1.5 }
        )
    ]
    [InlineData(
        new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0 }, 
        15.0, 
        new[] { 8.0, 1.0, 6.0 },
        new[] { 3.0, 5.0, 7.0 },
        new[] { 4.0, 9.0, 2.0 }
        )
    ]
    public void CalculateMagicNumber_WhenGivenCorrectNumberOfValues_ReturnsCorrectNumber(
        double[] values,
        double expectedMagicNumber,
        double[] expectedFirstRow,
        double[] expectedSecondRow,
        double[] expectedThirdRow)
    {
        var magicSquareCalculator = new MagicSquareCalculator();
        var result = magicSquareCalculator.CalculateMagicNumber(values);
        
        result.MagicNumber.ShouldNotBeNull();
        result.MagicNumber.ShouldBe(expectedMagicNumber);
        result.Grid.ShouldNotBeNull();

        var expectedGrid = new List<double[]>()
        {
            expectedFirstRow,
            expectedSecondRow,
            expectedThirdRow
        };

        for (var i = 0; i < result.Grid.GetLength(0); i++)
        {
            for (var j = 0; j < result.Grid.GetLength(1); j++)
            {
                result.Grid.GetValue(i, j).ShouldBe(expectedGrid[i][j]);
            }
        }
    }
}
