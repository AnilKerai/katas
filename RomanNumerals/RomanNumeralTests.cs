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
}
