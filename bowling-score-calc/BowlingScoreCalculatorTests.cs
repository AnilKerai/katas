namespace bowling_score_calc;

using Xunit;
using System;

public class BowlingScoreCalculatorTests
{
    [Theory]
    [InlineData("9", 9)]
    [InlineData("92", 11)]
    [InlineData("9x2", 13)]
    [InlineData("7x4", 15)]
    [InlineData("x46", 14)]
    [InlineData("/1", 2)]
    [InlineData("8/2", 11)]
    [InlineData("//////////", 289)]
    [InlineData("xxxxxxxxxx", 10240)]
    public void Calculate_ShouldReturnCurrentScore_WhenGivenString(string scores, int expectedScore)
    {
        var sut = new BowlingScoreCalculator();
        var result = sut.Calculate(scores);
        Assert.Equal(expectedScore, result);
    }

    [Fact]
    public void Calculate_ShouldThrowArguementException_WhenGivenAnInvalidString()
    {
        var sut = new BowlingScoreCalculator();
        Assert.Throws<ArgumentException>(() => sut.Calculate("qwetuio"));
    }

    [Fact]
    public void Calculate_ShouldThrowArguementException_WhenGivenAStringLongerThanTenCharacters()
    {
        var sut = new BowlingScoreCalculator();
        Assert.Throws<ArgumentException>(() => sut.Calculate("qwertyuiopq"));
    }
}