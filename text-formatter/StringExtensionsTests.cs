namespace text_formatter;

public class StringExtensionsTests
{
    [Fact]
    public void GetLines_ShouldReturnZeroLines_WhenStringIsEmpty()
    {
        var result = string.Empty.GetLines();
        result.Count().ShouldBe(0);
    }
    
    [Theory, AutoNSubstituteData]
    public void GetLines_ShouldReturnOneLine_WhenValidStringHasNoConfiguredSeparators(
        string randomString
    )
    {
        var result = randomString.GetLines();
        result.Count().ShouldBe(1);
    }
    
    [Theory]
    [InlineAutoNSubstituteData("qwe,wer ,ertyu,", 3)]
    [InlineAutoNSubstituteData("qwe,,wer", 2)]
    public void GetLines_ShouldReturnCorrectLineCount_WhenValidStringHasConfiguredSeparators(
        string sut,
        int expectedLineCount
    )
    {
        var result = sut.GetLines();
        result.Count().ShouldBe(expectedLineCount);
    }
    
    [Fact]
    public void GetPaddedLine_ShouldReturnNewLine_WhenGivenEmptyString()
    {
        var result = string.Empty.GetPaddedLine(3);
        result.ShouldBe("\n");
    }
    
    [Theory]
    [InlineAutoNSubstituteData("$a", "a  \n", 3)]
    [InlineAutoNSubstituteData("a$b$", "a  b  \n", 3)]
    public void GetPaddedLine_ShouldReturnPaddedLine_WhenGivenValidStringAndMaxColumnSize(
        string sut,
        string expectedPaddedLine,
        int maxColumnSize
    )
    {
        var result = sut.GetPaddedLine(maxColumnSize);
        result.ShouldBe(expectedPaddedLine);
    }
    
    [Fact]
    public void GetLengthOfLargestWordInString_ShouldReturn0_WhenGivenEmptyString()
    {
        var result = string.Empty.GetLengthOfLargestWordInString();
        result.ShouldBe(0);
    }
    
    [Theory]
    [InlineAutoNSubstituteData("$a", 1)]
    [InlineAutoNSubstituteData("a$qwertyb$dgbvh", 7)]
    public void GetLengthOfLargestWordInString_ShouldReturnPaddedLine_WhenGivenValidStringAndMaxColumnSize(
        string sut,
        int expectedWordLength
    )
    {
        var result = sut.GetLengthOfLargestWordInString();
        result.ShouldBe(expectedWordLength);
    }
}