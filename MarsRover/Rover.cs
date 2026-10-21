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

    private static readonly string[] DirectionNames = ["N", "E", "S", "W"];

    private int _x;
    private int _y;
    private int _direction;

    /// <summary>
    /// Runs <paramref name="commands"/> in order and returns the final position as <c>x:y:D</c>.
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
                    _x += Moves[_direction].DX;
                    _y += Moves[_direction].DY;
                    break;
            }
        }

        return $"{_x}:{_y}:{DirectionNames[_direction]}";
    }
}