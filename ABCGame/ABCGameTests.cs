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

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenGivenNullBlocks()
    {
        var ex = Should.Throw<ArgumentNullException>(() => new AbcGame(null!));
        ex.ParamName.ShouldBe("userSelectedBlocks");
    }

    [Theory]
    [InlineData("AC", true)]
    [InlineData("BD", true)]
    [InlineData("AB", false)] // both letters are on the same block
    [InlineData("E", false)]  // the default set has an E block, so these blocks replace it rather than add to it
    public void CanMakeWord_ShouldUseOnlyTheGivenBlocks_WhenBlocksAreSupplied(string word, bool expected)
    {
        var sut = new AbcGame([new Block("A", "B"), new Block("C", "D")]);

        sut.CanMakeWord(word).ShouldBe(expected);
    }

    [Theory]
    [InlineData("A", true)]
    [InlineData("AA", false)] // a block still spells only one letter when both faces show the same letter
    public void CanMakeWord_ShouldSpendOneBlockPerLetter_WhenBothFacesOfABlockShowTheSameLetter(string word, bool expected)
    {
        var sut = new AbcGame([new Block("A", "A")]);

        sut.CanMakeWord(word).ShouldBe(expected);
    }
}