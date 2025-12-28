namespace Reordering;

public class ReordererTests
{
    [Theory]
    [InlineData(
        new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
        -1
        )
    ]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            11
        )
    ]
    public void Reorder_WhenStartIndexIsOutsideOfInputArrayBounds_ThrowArgumentOutOfRangeException(char[] input, int start)
    {
        var action = () => GetReorderer().Reorder(input, start, 3, 8);
        action.ShouldThrow<ArgumentOutOfRangeException>(nameof(start));
    }
    
    [Theory]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            -1
        )
    ]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            11
        )
    ]
    public void Reorder_WhenEndIndexIsOutsideOfInputArrayBounds_ThrowArgumentOutOfRangeException(char[] input, int end)
    {
        var action = () => GetReorderer().Reorder(input, 2, end, 8);
        action.ShouldThrow<ArgumentOutOfRangeException>(nameof(end));
    }
    
    [Theory]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            8,
            6
        )
    ]
    public void Reorder_WhenEndIndexIsLessThanStartIndex_ThrowArgumentException(char[] input, int start, int end)
    {
        var action = () => GetReorderer().Reorder(input, start, end, 8);
        action.ShouldThrow<ArgumentException>($"{nameof(end)} must be greater than {nameof(start)}");
    }
    
    [Theory]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            12
        )
    ]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            -1
        )
    ]
    public void Reorder_WhenDestinationIndexIsNotWithinArrayCountPlusOne_ThrowArgumentOutOfRangeException(char[] input, int destination)
    {
        var action = () => GetReorderer().Reorder(input, 2, 8, destination);
        action.ShouldThrow<ArgumentOutOfRangeException>(nameof(destination));
    }
    
    [Theory]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' },
            0,
            2,
            1
        )
    ]
    public void Reorder_WhenDestinationIndexIsWithinSelectedRange_ThrowArgumentException(char[] input, int start, int end, int destination)
    {
        var action = () => GetReorderer().Reorder(input, start, end, destination);
        action.ShouldThrow<ArgumentException>($"Destination ({destination}) index is within selected range ({start}-{end})");
    }
    
    [Theory]
    [InlineData(
        new [] { 'A', 'B', 'C', 'd', 'E', 'F', 'G', 'H', 'I', 'J' },
        new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }
        )
    ]
    public void Reorder_GivenArrayWithLowercaseCharacters_ReturnsResultInUppercase(char[] input, char[] expected)
    {
        var result = GetReorderer().Reorder(input, 0, 0, 0);
        result.ShouldBe(expected);
    }
    
    [Theory]
    [InlineData(
        new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }, 
        1, 
        3, 
        8, 
        new [] { 'A', 'E', 'F', 'G', 'H', 'B', 'C', 'D', 'I', 'J' }
        )
    ]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }, 
            4, 
            6, 
            0, 
            new [] { 'E', 'F', 'G', 'A', 'B', 'C', 'D', 'H', 'I', 'J' }
        )
    ]
    [InlineData(
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }, 
            7, 
            8, 
            10, 
            new [] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'J', 'H', 'I' }
        )
    ]
    public void Reorder_GivenValidInputArgs_ReturnsReorderedArray(char[] input, int start, int end, int destination, char[] expected)
    {
        var result = GetReorderer().Reorder(input, start, end, destination);
        Assert.Equal(expected, result);
    }
    
    private static Reorderer GetReorderer() => new();
}