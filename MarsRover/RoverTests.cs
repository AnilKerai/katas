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

    [Fact]
    public void Execute_WhenTheNextMoveWouldEnterAnObstacle_StopsOnTheLastFreeSquare()
    {
        var rover = new Rover((0, 3));

        rover.Execute("MMMM").ShouldBe("O:0:2:N");
    }

    [Fact]
    public void Execute_WhenAnObstacleBlocksACommand_IgnoresTheRemainingCommands()
    {
        var rover = new Rover((0, 1));

        rover.Execute("MRRR").ShouldBe("O:0:0:N");
    }

    [Fact]
    public void Execute_WhenCreatedWithSeveralObstacles_StopsAtTheFirstOneInTheWay()
    {
        // Rule 5 is a set of obstacles: only one on the rover's next square stops it.
        var rover = new Rover((0, 2), (0, 4));

        rover.Execute("MMMM").ShouldBe("O:0:1:N");
    }

    [Fact]
    public void Execute_WhenGivenNull_ThrowsArgumentNullException()
    {
        var rover = new Rover();

        Should.Throw<ArgumentNullException>(() => rover.Execute(null!));
    }

    [Theory]
    [InlineData("X")]
    [InlineData("l")]
    [InlineData("#")]
    public void Execute_WhenGivenAnUnknownCommand_ThrowsArgumentExceptionNamingIt(string commands)
    {
        var rover = new Rover();

        var exception = Should.Throw<ArgumentException>(() => rover.Execute(commands));

        exception.Message.ShouldContain($"'{commands[0]}'");
    }

    [Fact]
    public void Execute_WhenGivenAnUnknownCommand_DoesNotMoveAtAll()
    {
        var rover = new Rover();

        Should.Throw<ArgumentException>(() => rover.Execute("MX"));

        // The leading M never ran: the commands are validated before any movement.
        rover.Execute("").ShouldBe("0:0:N");
    }
}