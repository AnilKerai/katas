namespace Reordering;

public class Reorderer
{
    public char[] Reorder(char[] input, int start, int end, int destination)
    {
        var sanitizedInput = input.Select(char.ToUpper).ToList();
        
        if (start == end && destination == start)
            return sanitizedInput.ToArray();
        
        ValidateInputs(input, start, end, destination);
        
        var selectedRange = sanitizedInput.GetRange(start, end - start + 1);
        sanitizedInput.RemoveRange(start, end - start + 1);

        var newDestination = destination;
        
        if (destination > start)
            newDestination = destination - selectedRange.Count;
        
        sanitizedInput.InsertRange(newDestination, selectedRange);

        return sanitizedInput.ToArray();
    }

    private static void ValidateInputs(char[] input, int start, int end, int destination)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(start);
        
        ArgumentOutOfRangeException.ThrowIfGreaterThan(start, input.Length);

        ArgumentOutOfRangeException.ThrowIfNegative(end);
        
        ArgumentOutOfRangeException.ThrowIfGreaterThan(end, input.Length);

        if (end < start)
            throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
        
        if (destination < 0 || destination > input.Length + 1)
            throw new ArgumentOutOfRangeException(nameof(destination));
        
        if (destination >= start && destination <= end)
            throw new ArgumentException($"Destination ({destination}) index is within selected range ({start}-{end})");
    }
}