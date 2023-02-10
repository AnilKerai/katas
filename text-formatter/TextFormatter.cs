namespace text_formatter;

public class TextFormatter
{
    public string Format(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var largestWordLength = GetLengthOfLargestWordInString(text);

        var lines = 
            text
                .Split(',')
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList()
            ;

        var updatedLines = 
            lines
                .Select(line => GetPaddedLine(line, largestWordLength))
                .ToList();

        return string.Concat(updatedLines);
    }

    private static string GetPaddedLine(string line, int largestWordLength)
    {
        var splitWords =
            line
                .Split('$')
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList()
                ;
        
        var paddedWords = 
            splitWords
                .Select(s => GetPaddedWord(s, largestWordLength))
                .ToList()
            ;
        
        paddedWords.Add("\n");
        
        return string.Concat(paddedWords);
    }

    private static string GetPaddedWord(string word, int largestWordLength)
    {
        var numberOfPaddingChars = (largestWordLength + 2) - word.Length;
        var paddingWord = string.Concat(Enumerable.Repeat(' ', numberOfPaddingChars));
        return word + paddingWord;
    }
    
    private static int GetLengthOfLargestWordInString(string text)
    {
        return text
            .Split('$')
            .Select(s => s.Replace(",", string.Empty))
            .Max(s => s.Length)
            ;
    }
}