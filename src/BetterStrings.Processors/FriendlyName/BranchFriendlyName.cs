using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterStrings.Processors.FriendlyName;

public class BranchFriendlyNameProcessor
{
  public BranchFriendlyNameProcessor()
  {
    // todo implement rules
    // no special char, no spaces > replace with - instead of space
    // all lower case
  }

  public string Process(string input)
  {
    input = input.Replace("Task ", "task/");
    input = input.ToLower();
    input = input.Replace("ä", "ae");
    input = input.Replace("ö", "oe");
    input = input.Replace("ü", "ue");
    input = input.Replace(' ', '-');
    input = input.Replace(".", string.Empty);
    input = input.Replace(":", string.Empty);

    input = input.Substring(0, Math.Min(input.Length, 50));

    Console.WriteLine("Suggested Branch input:");

    Console.WriteLine(input);

    return input;
  }
}
