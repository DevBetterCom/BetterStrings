using BetterStrings.Processors.Crypto;
using Xunit;

namespace BetterStrings.UnitTests.Processors.Crypto
{
  public class HashProcessor_Process
  {
    [Theory]
    [InlineData("email", "0c83f57c786a0b4a39efab23731c7ebc")]
    [InlineData("devBetter", "516609b4a40ed15650ba1b850fdfd91d")]
    public void ReturnsExpectedHashStringGivenInput(string input, string expectedHash)
    {
      var processor = new HashProcessor();

      var result = processor.Process(input);

      Assert.Equal(expectedHash, result);
    }
  }
}
