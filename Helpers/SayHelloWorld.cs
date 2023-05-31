using ArmstrongServer.Models;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Helpers
{
  public static class SayHelloWorld
  {
    public static void Say()
    {
      System.Console.WriteLine(Constants.TextConstants.Licence);
      System.Console.WriteLine(Constants.TextConstants.GreetingsIntro);

      var config = SettingsHelper.GetConfiguration();
      var srvAttrConf = config.GetSection("ServerAttributes")
                              .Get<ServerAttributes>();
      var serverAttr = new ServerAttributes
      {
        Id = srvAttrConf.Id,
        Name = srvAttrConf.Name,
      };

      System.Console.WriteLine($"\tMy ID: {serverAttr.Id}");
      System.Console.WriteLine($"\tMy Name: {serverAttr.Name}");

      foreach (var ip in serverAttr.IPAddress)
        System.Console.WriteLine($"\tIP Address: {ip.ToString()}");

      System.Console.WriteLine(Constants.TextConstants.GreetingsOutro);
    }
  }
}
