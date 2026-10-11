namespace FizzBuzz;

public static class FizzBuzzer
{
    public static string Convert(int n) => n % 3 == 0 ? "Fizz" : n.ToString();
}
