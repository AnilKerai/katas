namespace text_formatter;

public static class StringExtensions
{
    public static IEnumerable<string> GetLines(this string text)
    {
        var lines =
                text
                    .Split(',', '.')
                    .Where(s => !string.IsNullOrEmpty(s))
            ;
        return lines;
    }

    public static string GetPaddedLine(this string line, int maxColumnSize)
    {
        var splitWords =
                line
                    .Split('$')
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToList()
            ;
        
        var paddedWords = 
                splitWords
                    .Select(s => s.GetPaddedWord(maxColumnSize))
                    .ToList()
            ;
        
        paddedWords.Add("\n");
        
        return string.Concat(paddedWords);
    }

    public static int GetLengthOfLargestWordInString(this string text)
    {
        return text
                .Split('$')
                .Select(s => s.Replace(",", string.Empty))
                .Select(s => s.Replace(".", string.Empty))
                .Max(s => s.Length)
            ;
    }
    
    private static string GetPaddedWord(this string word, int largestWordLength)
    {
        var numberOfPaddingChars = largestWordLength - word.Length;
        var paddingWord = string.Concat(Enumerable.Repeat(' ', numberOfPaddingChars));
        return word + paddingWord;
    }
}
