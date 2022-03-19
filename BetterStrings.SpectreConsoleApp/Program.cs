using Spectre.Console;
using Spectre.Console.Cli;

namespace SpectreConsoleApp;


public static class Program
{
  public static int Main(string[] args)
  {
    var app = new CommandApp();
    app.Configure(config =>
    {
      config.SetApplicationName("BetterStrings");
      config.ValidateExamples();
      config.AddExample(new[] { "run", "--no-build" });

      // Run
     // config.Add
 //     config.AddCommand<RunCommand>("run");

    });

    return app.Run(args);
  }
}
