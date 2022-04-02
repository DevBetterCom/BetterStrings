using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using BetterStrings.Processors.Crypto;
using BetterStrings.Processors.FriendlyName;


namespace BetterStrings.SpectreConsoleApp
{
  public class ProcessorCommand : Command<ProcessorSettings>
  {
    private static IServiceProvider _serviceProvider;


    private Serilog.ILogger CreateLogger(string logLevel)
    {
      var loggerLevel = logLevel.ToLower().ToSerilogEventLevel().Value;

      var loggerConfiguration = new LoggerConfiguration()
        .MinimumLevel.Is(loggerLevel)
        .Enrich.FromLogContext()
        .WriteTo.Console();

      var logger = loggerConfiguration.CreateLogger();
      Log.Logger = logger;

      logger.Debug("Logger created with level {0}", loggerLevel);

      return logger;
    }

    private static ServiceProvider SetupDependencyInjection(ConfigInfo configInfo, Serilog.ILogger logger)
    {
      var services = new ServiceCollection()
            .AddLogging()
            .AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(logger, false))
            .AddSingleton(configInfo)
            .AddSingleton<IneractiveMode>();

      return services.BuildServiceProvider();
    }

    public override int Execute(CommandContext context, ProcessorSettings settings)
    {
      var configInfo = new ConfigInfo(settings.LogLevel);

      var logger = CreateLogger(configInfo.LogLevel);

      logger.Debug("Logger Enabled");

      _serviceProvider = SetupDependencyInjection(configInfo, logger);

      logger.Debug("DI Setup Done");

      switch (settings.Mode)
      {
        case "interactive":
        case "i":
          var interactiveMode = _serviceProvider.GetService<IneractiveMode>();
          interactiveMode.Enter();
          break;
        case "command":
        case "c":
          break;
      }

      return 0;
    }


  }
}
