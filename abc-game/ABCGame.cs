namespace abc_game;

public record Block(string FirstLetter, string SecondLetter);

public class ABCGame
{
    private readonly List<Block> DEFAULT_BLOCKS = new()
    {
        { new Block("B", "O") },
        { new Block("X", "K") },
        { new Block("D", "Q") },
        { new Block("C", "P") },
        { new Block("N", "A") },
        { new Block("G", "T") },
        { new Block("R", "E") },
        { new Block("T", "G") },
        { new Block("Q", "D") },
        { new Block("F", "S") },
        { new Block("J", "W") },
        { new Block("H", "U") },
        { new Block("V", "I") },
        { new Block("A", "N") },
        { new Block("O", "B") },
        { new Block("E", "R") },
        { new Block("F", "S") },
        { new Block("L", "Y") },
        { new Block("P", "C") },
        { new Block("Z", "M") },
    };

    private readonly List<Block> _blocks;

    public ABCGame() => _blocks = DEFAULT_BLOCKS;

    public ABCGame(List<Block> userSelectedBlocks) => _blocks = userSelectedBlocks ?? throw new ArgumentNullException(nameof(userSelectedBlocks));

    public bool CanMakeWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;

        word = word.ToUpperInvariant();

        foreach (var letter in word)
        {
            var currentLetter = letter.ToString();
            var applicableBlocks = FindApplicableBlocks(currentLetter);

            if (!applicableBlocks.Any()) return false;

            var bestBlockToUse = applicableBlocks[0];

            foreach (var block in applicableBlocks)
            {
                string otherLetterOnBlock = GetOtherLetterOnBlock(currentLetter, block);

                if (otherLetterOnBlock.Equals(currentLetter) || word.Contains(otherLetterOnBlock))
                    continue;

                bestBlockToUse = block;
            }

            _blocks.Remove(bestBlockToUse);
        }

        return true;
    }

    private static string GetOtherLetterOnBlock(string letterString, Block block)
    {
        return !block.FirstLetter.Equals(letterString) ? block.FirstLetter : block.SecondLetter;
    }

    private List<Block> FindApplicableBlocks(string letterString)
    {
        return _blocks.Where(
            b => b.FirstLetter.Equals(letterString) || b.SecondLetter.Equals(letterString)
        ).ToList();
    }
}
