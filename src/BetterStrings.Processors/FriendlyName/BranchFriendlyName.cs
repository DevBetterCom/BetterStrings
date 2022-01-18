using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterStrings.Processors
{
  public class BranchFriendlyName
  {
    public BranchFriendlyName()
    {
      // todo implement rules
      // no special char, no spaces > replace with - instead of space
      // all lower case
    }

    public string Process(string input)
    {
      return input;
    }
  }
}
