using System.ComponentModel;
using Spectre.Console.Cli;

namespace BetterStrings.SpectreConsoleApp
{
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
}
