using System.Text;

namespace RomanNumerals;

public static class RomanNumeral
{
    private const int LargestNumber = 3999;

    // Subtractive pairs sit in the table as symbols in their own right, so one largest-first pass handles them.
    private static readonly (int Value, string Symbol)[] Symbols =
    [
        (1000, "M"),
        (900, "CM"),
        (500, "D"),
        (400, "CD"),
        (100, "C"),
        (90, "XC"),
        (50, "L"),
        (40, "XL"),
        (10, "X"),
        (9, "IX"),
        (5, "V"),
        (4, "IV"),
        (1, "I")
    ];

    public static string From(int number)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(number, LargestNumber);

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
