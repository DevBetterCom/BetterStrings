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
  //Input: Task 2: This is my test task

  //Output: -> task/2/this-is-my-test-task
  public string Process(string input)
  {
    string[] parts = input.Split(new string[] { ":", " " }, StringSplitOptions.RemoveEmptyEntries);

    string friendlyName = $"{parts[0]}/{parts[1]}/{parts[2]}";

    for (int i = 3; i < parts.Length; i++)
    {
      friendlyName += "-";
      friendlyName += parts[i];
    }

    friendlyName = friendlyName.ToLower();

    friendlyName = friendlyName.Replace("ä", "ae");
    friendlyName = friendlyName.Replace("ö", "oe");
    friendlyName = friendlyName.Replace("ü", "ue");

    friendlyName = friendlyName.Substring(0, Math.Min(friendlyName.Length, 50));

    return friendlyName;
  }
}
