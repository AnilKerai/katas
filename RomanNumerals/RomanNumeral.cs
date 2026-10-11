using System.Text;

namespace RomanNumerals;

public static class RomanNumeral
{
    // Subtractive pairs sit in the table as symbols in their own right, so one largest-first pass handles them.
    private static readonly (int Value, string Symbol)[] Symbols =
    [
        (5, "V"),
        (4, "IV"),
        (1, "I")
    ];

    public static string From(int number)
    {
        var numeral = new StringBuilder();
        var remaining = number;

        foreach (var (value, symbol) in Symbols)
        {
            while (remaining >= value)
            {
                numeral.Append(symbol);
                remaining -= value;
            }
        }

        return numeral.ToString();
    }
}
