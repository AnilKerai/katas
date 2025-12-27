namespace text_formatter;

public class TextFormatterTests
{
    [Theory, AutoNSubstituteData]
    public void Format_ShouldReturnString_WhenGivenValidInput(
        string randomString
    )
    {
        var returnValue = TextFormatter.Format(randomString);
        returnValue.ShouldBeOfType<string>();
    }
    
    [Fact]
    public void Format_ShouldReturnEmptyString_WhenGivenNullInput()
    {
        var returnValue = TextFormatter.Format(null!);
        returnValue.ShouldBeNullOrEmpty();
    }

    [Theory]
    [InlineAutoNSubstituteData("$Given", "Given  \n")]
    [InlineAutoNSubstituteData("Given$a$text$file$of$many$lines", "Given  a      text   file   of     many   lines  \n")]
    [InlineAutoNSubstituteData("Given$a$text,$file$of$many$lines", "Given  a      text   \nfile   of     many   lines  \n")]
    [InlineAutoNSubstituteData("Given$a$text.$file$of$many$lines", "Given  a      text   \nfile   of     many   lines  \n")]
    public void Format_ShouldReturnWordsInColumnsWithPadding_WhenGivenMultipleDollarSignSeparatedWords(
        string inputText,
        string expectedText
    )
    {
        var returnValue = TextFormatter.Format(inputText);
        returnValue.ShouldBe(expectedText);
    }
}