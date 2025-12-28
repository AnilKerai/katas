namespace TextFormatter;

public static class TextFormatter
{
    private const int MIN_PADDING = 2;
    public static string Format(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var maxColumnSize = text.GetLengthOfLargestWordInString() + MIN_PADDING;

        var lines = text.GetLines();

        var updatedLines = 
            lines
                .Select(line => line.GetPaddedLine(maxColumnSize))
                .ToList();

        return string.Concat(updatedLines);
    }
}