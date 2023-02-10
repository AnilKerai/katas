namespace text_formatter;

public class TextFormatterTests
{
    [Theory, AutoNSubstituteData]
    public void Format_ShouldReturnString_WhenGivenValidInput(
        string randomString,
        TextFormatter sut
    )
    {
        var returnValue = sut.Format(randomString);
        returnValue.Should().BeOfType<string>();
    }
    
    [Theory, AutoNSubstituteData]
    public void Format_ShouldReturnEmptyString_WhenGivenNullInput(
        TextFormatter sut
        )
    {
        var returnValue = sut.Format(null);
        returnValue.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineAutoNSubstituteData("$Given", "Given  \n")]
    [InlineAutoNSubstituteData("Given$a$text$file$of$many$lines", "Given  a      text   file   of     many   lines  \n")]
    [InlineAutoNSubstituteData("Given$a$text,$file$of$many$lines", "Given  a      text   \nfile   of     many   lines  \n")]
    public void Format_ShouldReturnWordsInColumnsWithPadding_WhenGivenMultipleDollarSignSeparatedWords(
        string inputText,
        string expectedText,
        TextFormatter sut
    )
    {
        var returnValue = sut.Format(inputText);
        returnValue.Should().Be(expectedText);
    }
}