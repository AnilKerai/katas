namespace MarsRover;

/// <summary>A rover exploring a grid, one command at a time.</summary>
public class Rover
{
    // Indexed by direction: north, east, south, west. Turning right steps one entry forward through
    // the list, turning left steps back.
    private static readonly (int DX, int DY)[] Moves =
    [
        (0, 1), (1, 0), (0, -1), (-1, 0)
    ];

    private const int GridSize = 10;

    private static readonly string[] DirectionNames = ["N", "E", "S", "W"];

    private int _x;
    private int _y;
    private int _direction;

    private readonly HashSet<(int X, int Y)> _obstacles;

    /// <summary>Creates a rover at <c>0:0:N</c>, avoiding <paramref name="obstacles"/> when moving.</summary>
    public Rover(params (int X, int Y)[] obstacles) => _obstacles = [.. obstacles];

    /// <summary>
    /// Runs <paramref name="commands"/> in order and returns the final position as <c>x:y:D</c>.
    /// When a move would enter an obstacle, the rover stops there, ignores the remaining commands and
    /// returns <c>O:x:y:D</c> instead.
    /// </summary>
    public string Execute(string commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case 'L':
                    _direction = (_direction + 3) % 4;
                    break;
                case 'R':
                    _direction = (_direction + 1) % 4;
                    break;
                case 'M':
                    if (!TryMoveForward())
                        return $"O:{_x}:{_y}:{DirectionNames[_direction]}";
                    break;
            }
        }

        return $"{_x}:{_y}:{DirectionNames[_direction]}";
    }

    // Moves one square forward, wrapping around the edges; reports false (and stays put) when the next
    // square holds an obstacle.
    private bool TryMoveForward()
    {
        var (dx, dy) = Moves[_direction];
        // The grid wraps, so both axes are taken modulo the grid size (plus it, to keep the remainder
        // positive when moving west or south).
        var x = ((_x + dx) % GridSize + GridSize) % GridSize;
        var y = ((_y + dy) % GridSize + GridSize) % GridSize;
        if (_obstacles.Contains((x, y)))
            return false;

        _x = x;
        _y = y;
        return true;
    }
}