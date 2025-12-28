namespace ABCGame;

public class AbcGameTests
{
    [Theory, AutoNSubstituteData]
    public void CanMakeWord_ShouldReturnFalse_WhenGivenEmptyInput(
        AbcGame sut
        )
    {
        var result = sut.CanMakeWord(string.Empty);
        result.ShouldBeFalse();
    }

    [Theory]
    [InlineAutoNSubstituteData("a")]
    [InlineAutoNSubstituteData("A")]
    public void CanMakeWord_ShouldReturnTrue_WhenGivenSingleLetterWordInAllowedBlocks(
        string word,
        AbcGame sut
        )
    {
        var result = sut.CanMakeWord(word);
        result.ShouldBeTrue();
    }

    [Theory]
    [InlineAutoNSubstituteData("book")]
    [InlineAutoNSubstituteData("coMMoN")]
    public void CanMakeWord_ShouldReturnFalse_WhenAllowedBlockHasBeenUsed(
        string word,
        AbcGame sut
        )
    {
        var result = sut.CanMakeWord(word);
        result.ShouldBeFalse();
    }

    [Theory]
    [InlineAutoNSubstituteData("bark")]
    [InlineAutoNSubstituteData("TReaT")]
    [InlineAutoNSubstituteData("SQUAD")]
    [InlineAutoNSubstituteData("confuse")]
    public void CanMakeWord_ShouldReturnTrue_WhenAllowedBlockHasBeenUsed(
        string word,
        AbcGame sut
        )
    {
        var result = sut.CanMakeWord(word);
        result.ShouldBeTrue();
    }
}