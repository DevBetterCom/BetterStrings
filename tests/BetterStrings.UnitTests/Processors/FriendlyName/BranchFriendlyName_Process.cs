using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterStrings.Processors.FriendlyName;
using Xunit;

namespace BetterStrings.UnitTests.Processors.FriendlyName
{
  public class BranchFriendlyName_Process
  {
    [Theory]
    [InlineData("Task 2: This is my test task", "task/2/this-is-my-test-task")]
    [InlineData("Task 2: Hä Hö Hü", "task/2/hae-hoe-hue")]
    public void ReturnsFriendlyNameForGivenInput(string input, string expectedResult)
    {
      var processor = new BranchFriendlyNameProcessor();

      var result = processor.Process(input);

      Assert.Equal(expectedResult, result);
    }
  }

}
