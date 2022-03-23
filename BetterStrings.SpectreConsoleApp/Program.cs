using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace BetterStrings.SpectreConsoleApp;

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
