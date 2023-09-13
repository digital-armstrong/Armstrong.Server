using ArmstrongServer.Models;
using ArmstrongServer.Models.ConfigModels;

namespace ArmstrongServer.Helpers
{
  public static class SayHelloWorld
  {
    public static void Say()
    {
      System.Console.WriteLine(Constants.TextConstants.Licence);
      System.Console.WriteLine(Constants.TextConstants.GreetingsIntro);

      System.Console.WriteLine($"\tServers count: {AppSettings.AppGeneralSettings.ServerProps.Count}");

      System.Console.WriteLine(Constants.TextConstants.GreetingsBody);
      System.Console.WriteLine(Constants.TextConstants.GreetingsOutro);
    }
  }
}
