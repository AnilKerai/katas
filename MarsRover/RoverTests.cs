namespace MarsRover;

public class RoverTests
{
    [Fact]
    public void Execute_WhenNoCommandsAreGiven_ReturnsTheRoversCurrentPosition()
    {
        var rover = new Rover();

        rover.Execute("").ShouldBe("0:0:N");

        rover.Execute("M");

        rover.Execute("").ShouldBe("0:1:N");
    }

    [Theory]
    [InlineData("L", "0:0:W")]
    [InlineData("R", "0:0:E")]
    [InlineData("M", "0:1:N")]
    public void Execute_WhenGivenASingleCommand_TurnsOrMovesOnce(string commands, string expected)
    {
        var rover = new Rover();

        rover.Execute(commands).ShouldBe(expected);
    }

    [Theory]
    [InlineData("MMRMMLM", "2:3:N")]
    [InlineData("MMMMMMMMMM", "0:0:N")]
    [InlineData("LM", "9:0:W")]
    [InlineData("RRM", "0:9:S")]
    [InlineData("", "0:0:N")]
    public void Execute_WhenGivenTheKataExamplesWithoutObstacles_ReturnsTheExpectedPosition(
        string commands, string expected)
    {
        var rover = new Rover();

        rover.Execute(commands).ShouldBe(expected);
    }

    [Fact]
    public void Execute_WhenMovingEastAcrossTheEdge_WrapsToTheWestSide()
    {
        var rover = new Rover();

        rover.Execute("RMMMMMMMMMM").ShouldBe("0:0:E");
    }
}