using Spectre.Console.Cli;
using BetterStrings.SpectreConsoleApp;
using Spectre.Console;

public static class Program
{
  public static int Main(string[] args)
  {
    AnsiConsole.Write(new Rule("[blue]Welcome to BetterString[/]"));

    var app = new CommandApp<ProcessorCommand>();
    app.Configure(config =>
    {
      config.SetApplicationName("BetterStrings");

    });
    return app.Run(args);
  }
}
