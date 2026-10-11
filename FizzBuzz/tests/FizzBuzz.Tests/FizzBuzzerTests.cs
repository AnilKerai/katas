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

    [Fact]
    public void Convert_ShouldReturnBuzz_WhenInputIs5()
    {
        FizzBuzzer.Convert(5).ShouldBe("Buzz");
    }

    [Fact]
    public void Convert_ShouldReturnBuzz_WhenInputIs10()
    {
        FizzBuzzer.Convert(10).ShouldBe("Buzz");
    }

    [Fact]
    public void Convert_ShouldReturnFizzBuzz_WhenInputIs15()
    {
        FizzBuzzer.Convert(15).ShouldBe("FizzBuzz");
    }
}
