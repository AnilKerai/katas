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
    [InlineData(10, "X")]
    [InlineData(50, "L")]
    [InlineData(100, "C")]
    [InlineData(500, "D")]
    [InlineData(1000, "M")]
    [InlineData(2026, "MMXXVI")]
    public void From_ShouldWriteSymbolsLargestFirst_WhenNumberIsASumOfSymbols(int number, string expected)
    {
        RomanNumeral.From(number).ShouldBe(expected);
    }

    [Theory]
    [InlineData(4, "IV")]
    [InlineData(9, "IX")]
    [InlineData(40, "XL")]
    [InlineData(90, "XC")]
    [InlineData(400, "CD")]
    [InlineData(900, "CM")]
    public void From_ShouldSubtractSmallerSymbolWrittenFirst_WhenSymbolWouldRepeatFourTimes(int number, string expected)
    {
        RomanNumeral.From(number).ShouldBe(expected);
    }

    [Theory]
    [InlineData(14, "XIV")]
    [InlineData(1994, "MCMXCIV")]
    [InlineData(3999, "MMMCMXCIX")]
    public void From_ShouldCombineAddedAndSubtractedSymbols_WhenNumberNeedsBoth(int number, string expected)
    {
        RomanNumeral.From(number).ShouldBe(expected);
    }
}
