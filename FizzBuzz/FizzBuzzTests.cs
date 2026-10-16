namespace FizzBuzz;

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

// Runs Program.cs through the assembly's entry point, which the test runner never calls. Console redirection is
// process-wide, so these tests share a collection and cannot overlap one another; nothing else here writes to the console.
[Collection("Console")]
public class ProgramTests
{
    private const string Usage = "Usage: FizzBuzz [upperBound]  (upperBound must be a positive integer)";

    [Fact]
    public void Main_ShouldPrintOneToOneHundred_WhenNoArgumentIsGiven()
    {
        var result = Run();

        result.ExitCode.ShouldBe(0);
        result.Error.ShouldBeEmpty();
        var lines = Lines(result.Output);
        lines.Length.ShouldBe(100);
        lines[0].ShouldBe("1");
        lines[14].ShouldBe("FizzBuzz");
        lines[99].ShouldBe("Buzz");
    }

    [Fact]
    public void Main_ShouldPrintOneToTheArgument_WhenArgumentIsAPositiveInteger()
    {
        string[] expected =
        [
            "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"
        ];

        var result = Run("15");

        result.ExitCode.ShouldBe(0);
        result.Error.ShouldBeEmpty();
        Lines(result.Output).ShouldBe(expected);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("abc")]
    [InlineData("-5")] // starts with a dash but is an integer, so it is not a runner option
    public void Main_ShouldPrintUsageAndReturnOne_WhenArgumentIsNotAPositiveInteger(string argument)
    {
        var result = Run(argument);

        result.ExitCode.ShouldBe(1);
        result.Error.Trim().ShouldBe(Usage);
        result.Output.ShouldBeEmpty();
    }

    [Fact]
    public void Main_ShouldHandOverToTheXunitRunner_WhenFirstArgumentIsARunnerOption()
    {
        var result = Run("-list", "classes");

        result.ExitCode.ShouldBe(0);
        result.Output.ShouldContain("FizzBuzz.FizzBuzzerTests");
        result.Error.ShouldNotContain(Usage);
    }

    private static string[] Lines(string output) =>
        output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    private static (int ExitCode, string Output, string Error) Run(params string[] args)
    {
        var entryPoint = typeof(FizzBuzzer).Assembly.EntryPoint!;
        var originalOut = Console.Out;
        var originalError = Console.Error;
        using var output = new StringWriter();
        using var error = new StringWriter();
        Console.SetOut(output);
        Console.SetError(error);
        try
        {
            var exitCode = (int)entryPoint.Invoke(null, [args])!;
            return (exitCode, output.ToString(), error.ToString());
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetError(originalError);
        }
    }
}
