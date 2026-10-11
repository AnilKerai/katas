namespace FizzBuzz;

public static class FizzBuzzer
{
    public static string Convert(int n)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        if (n % 15 == 0) return "FizzBuzz";
        if (n % 3 == 0) return "Fizz";
        if (n % 5 == 0) return "Buzz";
        return n.ToString();
    }

    public static IEnumerable<string> Sequence(int from, int to) =>
        Enumerable.Range(from, to - from + 1).Select(Convert);
}
