using Spectre.Console.Cli;
using BetterStrings.SpectreConsoleApp;

public static class Program
{
  public static int Main(string[] args)
  {
    var app = new CommandApp<ProcessorCommand>();
    app.Configure(config =>
    {
      config.SetApplicationName("BetterStrings");

    });
    return app.Run(args);
  }
}
