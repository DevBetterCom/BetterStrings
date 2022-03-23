﻿using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace BetterStrings.SpectreConsoleApp;

public class ProcessorSettings : CommandSettings
{
  [CommandArgument(0, "[PROCESSOR]")]
  public string Processor { get; set; }

  [CommandArgument(1, "[INPUT_STRING]")]
  public string InputString { get; set; }

  [CommandOption("-m|--mode")]
  [DefaultValue("interactive")]
  public string Mode { get; set; }

  [CommandOption("-v|--verbose")]
  public string LogLevel { get; set; }
}

public class ProcessorCommand : Command<ProcessorSettings>
{
  private static IServiceProvider _serviceProvider;
  public override int Execute(CommandContext context, ProcessorSettings settings)
  {
    switch (settings.Mode)
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
  private Serilog.ILogger CreateLogger(string logLevel)
  {
    var loggerLevel = logLevel.ToLower().ToLogEventLevel().Value;

    var loggerConfiguration = new LoggerConfiguration()
      .MinimumLevel.Is(loggerLevel)
      .Enrich.FromLogContext()
      .WriteTo.Console();

    var logger = loggerConfiguration.CreateLogger();
    Log.Logger = logger;

    logger.Debug("Logger created with level {0}", loggerLevel);

    return logger;
  }

  private static ServiceProvider SetupDi(ConfigInfo configInfo, Serilog.ILogger logger)
  {
    var services = new ServiceCollection()
          .AddLogging()
          .AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(logger, false))
          .AddSingleton(configInfo);

    return services.BuildServiceProvider();
  }

  public void EnterInteractiveMode(ProcessorSettings settings)
  {
    AnsiConsole.Write(new Rule("[blue]Welcome to BetterString[/]"));

    var configInfo = new ConfigInfo(settings.LogLevel);

    var logger = CreateLogger(configInfo.LogLevel);

    logger.Debug("Logger Enabled");

    _serviceProvider = SetupDi(configInfo, logger);

    logger.Debug("DI Setup Done");

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
