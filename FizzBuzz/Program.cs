using FizzBuzz;

// A first argument that starts with a dash and isn't an integer belongs to the test runner: `dotnet test` starts this
// executable with --server (or --results-directory, --help, ...). The branch below is the entry point xunit.v3
// generates for the other katas, except that the console runner is told the test assembly rather than left to take the
// entry assembly: when a host such as Rider's test runner loads this assembly, the entry assembly is the host. That
// also skips ConsoleRunner.Run's watchdog, which calls Environment.Exit after a delay and would kill such a host.
// An integer such as -5 is not a runner option and falls through to the usage check.
if (args.Length > 0 && args[0].StartsWith('-') && !int.TryParse(args[0], out _))
{
    if (global::System.Linq.Enumerable.Any(args, arg => arg == "--server" || arg == "--internal-msbuild-node"))
        return global::Xunit.MicrosoftTestingPlatform.TestPlatformTestFramework.RunAsync(args, global::FizzBuzz.SelfRegisteredExtensions.AddSelfRegisteredExtensions).GetAwaiter().GetResult();

    using var runner = new global::Xunit.Runner.InProc.SystemConsole.ConsoleRunner(args, typeof(FizzBuzzer).Assembly);
    return runner.EntryPoint().GetAwaiter().GetResult();
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
