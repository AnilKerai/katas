using System.Text;

namespace RomanNumerals;

/// <summary>Writes whole numbers as Roman numerals.</summary>
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

    /// <summary>Returns the Roman numeral for <paramref name="number"/>, for example <c>MCMXCIV</c> for 1994.</summary>
    /// <param name="number">A whole number from 1 to 3999.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="number"/> is less than 1 or greater than 3999.
    /// </exception>
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
