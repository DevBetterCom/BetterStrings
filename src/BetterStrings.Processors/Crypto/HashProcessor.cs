using System.Security.Cryptography;
using System.Text;

namespace BetterStrings.Processors.Crypto;

// TODO: Create a standard processor base type / interface
public class HashProcessor
{
  public HashProcessor()
  {
    // TODO: Inject in the algorithm to use
  }

  public string Process(string input)
  {
    using (var md5 = MD5.Create())
    {
      //var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
      //return Encoding.UTF8.GetString(result);

      byte[] retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < retVal.Length; i++)
      {
        sb.Append(retVal[i].ToString("x2"));
      }
      return sb.ToString();
    }
   
  }
}
