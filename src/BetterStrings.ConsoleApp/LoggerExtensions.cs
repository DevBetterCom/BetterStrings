using Serilog.Events;

namespace BetterStrings.ConsoleApp;

public static class LoggerExtensions
{
  public static LogEventLevel? ToLogEventLevel(this string level)
  {
    if (string.IsNullOrEmpty(level))
    {
      return null;
    }
    if (level == "error")
    {
      return LogEventLevel.Error;
    }
    if (level == "debug")
    {
      return LogEventLevel.Debug;
    }
    if (level == "trace")
    {
      return LogEventLevel.Verbose;
    }
    if (level == "info")
    {
      return LogEventLevel.Information;
    }
    if (level == "warning")
    {
      return LogEventLevel.Warning;
    }
    return LogEventLevel.Verbose;
  }
}

