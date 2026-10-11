using System.Text;

namespace RomanNumerals;

public static class RomanNumeral
{
    private static readonly (int Value, string Symbol)[] Symbols =
    [
        (5, "V"),
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
