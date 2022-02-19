using Microsoft.Extensions.DependencyInjection;
using McMaster.Extensions.CommandLineUtils;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using BetterStrings.Processors.Crypto;
using Spectre.Console;

namespace BetterStrings.ConsoleApp;
// See: https://briancaos.wordpress.com/2020/02/12/command-line-parameters-in-net-core-console-applications/

public class AsyncProgram
{
  // working from this example
  // https://github.com/natemcmaster/CommandLineUtils/blob/main/docs/samples/dependency-injection/generic-host/Program.cs

  private static IServiceProvider _serviceProvider;

  public static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<AsyncProgram>(args);

  [Option("-v|--verbose", Description = "Toggle logger verbosity: debug, trace, info, warning, error")]
  public string Verbose { get; } = "error";

  [Argument(0, Description = "Processor to apply")]
  public string Processor { get; } = "noop";

  [Argument(1, Description = "Input string")]
  public string InputString { get; } = "";

  [Option("-m|--mode", Description = "interactive/command: i, c")]
  public string Mode { get; } = "c";

  private async Task OnExecuteAsync() // do we need async for this app or should we just use sync?
  {
    AnsiConsole.Write(new Rule("[blue]Welcome to BetterString[/]"));

    var configInfo = new ConfigInfo("verbose");
    var logger = CreateLogger(configInfo.LogLevel);
    logger.Debug("Logger Enabled");
    _serviceProvider = SetupDi(configInfo, logger);
    logger.Debug("DI Setup Done");

    if (Mode == "i")
    {
      AnsiConsole.MarkupLine("[bold blue]Interactive mode...[/]");
      string userInputString = AnsiConsole.Ask<string>("Provide a string to transform.");


      var processorChoice = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
              .Title("Which processor do you want to use ?")
              .PageSize(10)
              .MoreChoicesText("[grey](Move up and down to reveal more processors)[/]")
              .AddChoices(new[] {
            "MD5", "Apricot", "Avocado",
            "Banana", "Blackcurrant", "Blueberry",
            "Cherry", "Cloudberry", "Cocunut",
              }));

      AnsiConsole.WriteLine($"I agree. {processorChoice} is tasty!");

      var processor = new HashProcessor();
      var result = processor.Process(userInputString);

      Console.WriteLine(result);
    }
    else
    {
      // call main service that has app logic using _serviceProvider to create it
      var processor = new HashProcessor();
      var result = processor.Process(InputString);

      Console.WriteLine(result);

    }


    // Console.WriteLine("Done, press any key to close");
    // Console.ReadKey();
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
}

