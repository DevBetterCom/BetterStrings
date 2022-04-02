using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using BetterStrings.Processors.Crypto;
using BetterStrings.Processors.FriendlyName;

namespace BetterStrings.SpectreConsoleApp
{
  public class IneractiveMode
  {
    public void Enter()
    {
      string result;


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
      var processor = AnsiConsole.Prompt(selection);

      AnsiConsole.WriteLine(processor);

      switch (processor)
      {
        case "MD5":
          var hashProcessor = new HashProcessor();
          result = hashProcessor.Process(userInputString);
          break;

        case "Branch Friendly Name":
          var branchProcessor = new BranchFriendlyNameProcessor();
          result = branchProcessor.Process(userInputString);
          break;

        default:
          result = "";
          break;
      }
      AnsiConsole.WriteLine(result);
    }
  }
}
