using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SpectreConsoleApp;

public class ProcessorSettings : CommandSettings
{
  [CommandArgument(0, "[PROCESSOR]")]
  public string Processor { get; set; }

  [CommandArgument(1, "[INPUT_STRING]")]
  public string InputString { get; set; }

  [CommandOption("-m|--mode")]
  [DefaultValue("interactive")]
  public string Mode { get; set; }
}

public class ProcessorCommand : Command<ProcessorSettings>
{
  public override int Execute(CommandContext context, ProcessorSettings settings)
  {
    switch(settings.Mode)
    {
      case "interactive":
      case "i":
        EnterInteractiveMode(settings);
        break;
      case "command":
      case "c":
        break;
    }

    return 0;
  }
  public void EnterInteractiveMode(ProcessorSettings settings)
  {
    AnsiConsole.MarkupLine("[bold blue]Interactive mode...[/]");

    string userInputString = AnsiConsole.Ask<string>("Provide a string to transform: ");

    var selection = new SelectionPrompt<string>()
      .Title("Which processor do you want to use ?")
      .PageSize(10)
      .MoreChoicesText("[grey](Move up and down to reveal more processors)[/]")
      .AddChoices(new[] {
            "MD5",
            "Branch Friendly Name"
      });
    settings.Processor = AnsiConsole.Prompt(selection);

    AnsiConsole.WriteLine(settings.Processor);
  }
}

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
