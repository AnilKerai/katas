namespace RomanNumerals;

public class RomanNumeralTests
{
    [Fact]
    public void From_ShouldReturnI_WhenNumberIsOne()
    {
        RomanNumeral.From(1).ShouldBe("I");
    }
}
