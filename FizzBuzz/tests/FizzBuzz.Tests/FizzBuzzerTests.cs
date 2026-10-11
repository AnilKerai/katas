namespace FizzBuzz.Tests;

public class FizzBuzzerTests
{
    [Fact]
    public void Convert_ShouldReturn1_WhenInputIs1()
    {
        FizzBuzzer.Convert(1).ShouldBe("1");
    }

    [Fact]
    public void Convert_ShouldReturn2_WhenInputIs2()
    {
        FizzBuzzer.Convert(2).ShouldBe("2");
    }

    [Fact]
    public void Convert_ShouldReturnFizz_WhenInputIs3()
    {
        FizzBuzzer.Convert(3).ShouldBe("Fizz");
    }

    [Fact]
    public void Convert_ShouldReturnFizz_WhenInputIs6()
    {
        FizzBuzzer.Convert(6).ShouldBe("Fizz");
    }
}
