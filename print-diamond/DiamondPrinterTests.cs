namespace print_diamond;

public class DiamondPrinterTests
{
    [Fact]
    public void DrawForLetter_ShouldThrowException_WhenGivenEmptyString(
    )
    {
        var action = () => DiamondPrinter.DrawForLetter(string.Empty);
        action.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [InlineAutoNSubstituteData("aL")]
    [InlineAutoNSubstituteData("Qfnwjonf")]
    public void DrawForLetter_ShouldThrowException_WhenGivenMultipleLetters(
        string input
    )
    {
        var action = () => DiamondPrinter.DrawForLetter(input);
        action.ShouldThrow<ArgumentException>("Input is not a single letter");
    }

    [Theory]
    [InlineAutoNSubstituteData("a", "A")]
    [InlineAutoNSubstituteData("b", ".A.\nB.B\n.A.")]
    [InlineAutoNSubstituteData("c", "..A..\n.B.B.\nC...C\n.B.B.\n..A..")]
    [InlineAutoNSubstituteData("e", "....A....\n...B.B...\n..C...C..\n.D.....D.\nE.......E\n.D.....D.\n..C...C..\n...B.B...\n....A....")]
    public void DrawForLetter_ShouldReturnDiamond_WhenGivenLetterInLowerCase(
        string letter,
        string expectedResult
    )
    {
        var result = DiamondPrinter.DrawForLetter(letter);
        result.ShouldBe(expectedResult);
    }

    [Theory]
    [InlineAutoNSubstituteData("A", "A")]
    [InlineAutoNSubstituteData("B", ".A.\nB.B\n.A.")]
    [InlineAutoNSubstituteData("C", "..A..\n.B.B.\nC...C\n.B.B.\n..A..")]
    [InlineAutoNSubstituteData("E", "....A....\n...B.B...\n..C...C..\n.D.....D.\nE.......E\n.D.....D.\n..C...C..\n...B.B...\n....A....")]
    public void DrawForLetter_ShouldReturnDiamond_WhenGivenLetterInUpperCase(
        string letter,
        string expectedResult
    )
    {
        var result = DiamondPrinter.DrawForLetter(letter);
        result.ShouldBe(expectedResult);
    }
}