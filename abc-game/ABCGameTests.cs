using FluentAssertions;
using Xunit;

namespace abc_game;

public class ABCGameTests
{
    [Theory, AutoNSubstituteData]
    public void CanMakeWord_ShouldReturnFalse_WhenGivenEmptyInput(
        ABCGame sut
        )
    {
        var result = sut.CanMakeWord(string.Empty);
        result.Should().BeFalse();
    }

    [Theory]
    [InlineAutoNSubstituteData("a")]
    [InlineAutoNSubstituteData("A")]
    public void CanMakeWord_ShouldReturnTrue_WhenGivenSingleLetterWordInAllowedBlocks(
        string word,
        ABCGame sut
        )
    {
        var result = sut.CanMakeWord(word);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineAutoNSubstituteData("book")]
    [InlineAutoNSubstituteData("coMMoN")]
    public void CanMakeWord_ShouldReturnFalse_WhenAllowedBlockHasBeenUsed(
        string word,
        ABCGame sut
        )
    {
        var result = sut.CanMakeWord(word);
        result.Should().BeFalse();
    }

    [Theory]
    [InlineAutoNSubstituteData("bark")]
    [InlineAutoNSubstituteData("TReaT")]
    [InlineAutoNSubstituteData("SQUAD")]
    [InlineAutoNSubstituteData("confuse")]
    public void CanMakeWord_ShouldReturnTrue_WhenAllowedBlockHasBeenUsed(
        string word,
        ABCGame sut
        )
    {
        var result = sut.CanMakeWord(word);
        result.Should().BeTrue();
    }
}