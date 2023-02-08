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
}