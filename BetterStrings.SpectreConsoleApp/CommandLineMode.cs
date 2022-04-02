using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using BetterStrings.Processors.FriendlyName;
using BetterStrings.Processors.Crypto;

namespace BetterStrings.SpectreConsoleApp
{
  public class CommandLineMode
  {
    public string ProcessorName { get; set; }
    public string InputSring { get; set; }
    public void Enter()
    {
      string result;

      AnsiConsole.MarkupLine("[bold blue]Command line mode...[/]");

      AnsiConsole.WriteLine(ProcessorName);

      switch (ProcessorName)
      {
        case "md5":
          var hashProcessor = new HashProcessor();
          result = hashProcessor.Process(InputSring);
          break;

        case "branch-friendly-name":
          var branchProcessor = new BranchFriendlyNameProcessor();
          result = branchProcessor.Process(InputSring);
          break;

        default:
          result = "";
          break;
      }
      AnsiConsole.WriteLine(result);
    }
  }
}
