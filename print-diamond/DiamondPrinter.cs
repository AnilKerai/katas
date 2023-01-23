namespace print_diamond;

public static class DiamondPrinter
{
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const char PLACEHOLDER = '.';
    private const char NEW_LINE = '\n';

    public static string DrawForLetter(string outermostLetter)
    {
        if (string.IsNullOrEmpty(outermostLetter))
            throw new ArgumentNullException(nameof(outermostLetter));

        if (outermostLetter.Length != 1)
            throw new ArgumentException("Input is not a single letter");

        outermostLetter = outermostLetter.ToUpperInvariant();

        var orderedLettersForDiamond = GetOrderedLettersForDiamond(outermostLetter);
        var gridRows = new List<string>();

        foreach (var letterForCurrentRow in orderedLettersForDiamond)
        {
            var rowTemplate = BuildRowTemplate(orderedLettersForDiamond.Length);
            var populatedRow = GetPopulatedRowForLetter(rowTemplate, letterForCurrentRow, outermostLetter);
            gridRows.Add(new string(populatedRow));
        }

        return GetGridAsString(gridRows);
    }

    private static char[] BuildRowTemplate(int itemCount)
    {
        var rowArray = new char[itemCount];
        Array.Fill(rowArray, PLACEHOLDER);
        return rowArray;
    }

    private static string GetGridAsString(List<string> gridRows) => string.Join(NEW_LINE, gridRows);

    private static char[] GetPopulatedRowForLetter(
        char[] lineTemplate,
        char letterForCurrentRow,
        string outermostLetter
    )
    {
        var line = lineTemplate;
        var indexOfFinalLetter = ALPHABET.IndexOf(outermostLetter);
        var distinctLetters = ALPHABET[..(indexOfFinalLetter + 1)];
        var midpoint = (int)Math.Ceiling((double)line.Length / 2) - 1;
        var offset = distinctLetters.IndexOf(letterForCurrentRow);
        var leftOfMidpointUpdatePosition = midpoint - offset;
        var rightOfMidpointUpdatePosition = midpoint + offset;

        line[leftOfMidpointUpdatePosition] = letterForCurrentRow;
        line[rightOfMidpointUpdatePosition] = letterForCurrentRow;

        return line;
    }

    private static string GetOrderedLettersForDiamond(string letter)
    {
        var orderedLetters = ALPHABET[..ALPHABET.IndexOf(letter)];
        var orderedLettersCharArray = orderedLetters.ToCharArray();
        Array.Reverse(orderedLettersCharArray);
        var reverseOrderedLetters = new string(orderedLettersCharArray);
        orderedLetters += letter;
        orderedLetters += reverseOrderedLetters;
        return orderedLetters;
    }
}