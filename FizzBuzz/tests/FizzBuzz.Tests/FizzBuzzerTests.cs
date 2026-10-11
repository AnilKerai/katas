namespace FizzBuzz.Tests;

public class FizzBuzzerTests
{
    [Fact]
    public void Convert_ShouldReturn1_WhenInputIs1()
    {
        FizzBuzzer.Convert(1).ShouldBe("1");
    }
}
