namespace RomanNumerals;

public class RomanNumeralTests
{
    [Theory]
    [InlineData(1, "I")]
    [InlineData(2, "II")]
    [InlineData(3, "III")]
    public void From_ShouldRepeatSymbol_WhenNumberIsUpToThreeTimesItsValue(int number, string expected)
    {
        RomanNumeral.From(number).ShouldBe(expected);
    }

    [Theory]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(8, "VIII")]
    public void From_ShouldWriteSymbolsLargestFirst_WhenNumberIsASumOfSymbols(int number, string expected)
    {
        RomanNumeral.From(number).ShouldBe(expected);
    }

    [Theory]
    [InlineData(4, "IV")]
    public void From_ShouldSubtractSmallerSymbolWrittenFirst_WhenSymbolWouldRepeatFourTimes(int number, string expected)
    {
        RomanNumeral.From(number).ShouldBe(expected);
    }
}
