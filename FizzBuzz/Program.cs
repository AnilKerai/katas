using FizzBuzz;

// A first argument that starts with a dash and isn't an integer belongs to the test runner: `dotnet test` starts this
// executable with --server (or --results-directory, --help, ...). The branch below is the entry point xunit.v3
// generates for the other katas. An integer such as -5 is not a runner option and falls through to the usage check.
if (args.Length > 0 && args[0].StartsWith('-') && !int.TryParse(args[0], out _))
{
    if (global::System.Linq.Enumerable.Any(args, arg => arg == "--server" || arg == "--internal-msbuild-node"))
        return global::Xunit.MicrosoftTestingPlatform.TestPlatformTestFramework.RunAsync(args, global::FizzBuzz.SelfRegisteredExtensions.AddSelfRegisteredExtensions).GetAwaiter().GetResult();
    else
        return global::Xunit.Runner.InProc.SystemConsole.ConsoleRunner.Run(args).GetAwaiter().GetResult();
}

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
