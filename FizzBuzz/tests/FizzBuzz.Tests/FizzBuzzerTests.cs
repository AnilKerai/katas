namespace FizzBuzz.Tests;

public class FizzBuzzerTests
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    public void Convert_ShouldReturnNumber_WhenNotDivisibleByThreeOrFive(int n, string expected)
    {
        FizzBuzzer.Convert(n).ShouldBe(expected);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    public void Convert_ShouldReturnFizz_WhenDivisibleByThree(int n)
    {
        FizzBuzzer.Convert(n).ShouldBe("Fizz");
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void Convert_ShouldReturnBuzz_WhenDivisibleByFive(int n)
    {
        FizzBuzzer.Convert(n).ShouldBe("Buzz");
    }

    [Theory]
    [InlineData(15)]
    [InlineData(30)]
    public void Convert_ShouldReturnFizzBuzz_WhenDivisibleByFifteen(int n)
    {
        FizzBuzzer.Convert(n).ShouldBe("FizzBuzz");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Convert_ShouldThrowArgumentOutOfRange_WhenNotPositive(int n)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => FizzBuzzer.Convert(n));
    }

    [Fact]
    public void Sequence_ShouldReturnFizzBuzzValues_WhenRangeIsOneToFifteen()
    {
        string[] expected =
        [
            "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"
        ];

        FizzBuzzer.Sequence(1, 15).ShouldBe(expected);
    }

    [Theory]
    [InlineData(0, 5)]
    [InlineData(5, 4)]
    public void Sequence_ShouldThrowArgumentOutOfRange_WhenCalledWithInvalidRange(int first, int last)
    {
        // Not enumerated: validation must happen eagerly, on the call itself.
        Should.Throw<ArgumentOutOfRangeException>(() => FizzBuzzer.Sequence(first, last));
    }
}
