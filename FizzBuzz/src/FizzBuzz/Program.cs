using FizzBuzz;

var upperBound = 100;

if (args.Length > 0 && (!int.TryParse(args[0], out upperBound) || upperBound < 1))
{
    Console.Error.WriteLine("Usage: FizzBuzz [upperBound]  (upperBound must be a positive integer)");
    return 1;
}

foreach (var line in FizzBuzzer.Sequence(1, upperBound))
{
    Console.WriteLine(line);
}

return 0;
