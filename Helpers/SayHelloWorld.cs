using ArmstrongServer.Models;

namespace ArmstrongServer.Helpers
{
  public static class SayHelloWorld
  {
    public static void Say()
    {
      System.Console.WriteLine(Constants.TextConstants.Licence);
      System.Console.WriteLine(Constants.TextConstants.GreetingsIntro);

      System.Console.WriteLine($"\tMy ID: {AppSettings.AppServerAttributes.Id}");
      System.Console.WriteLine($"\tMy Name: {AppSettings.AppServerAttributes.Name}");

      foreach (var ip in AppSettings.AppServerAttributes.IPAddress)
        System.Console.WriteLine($"\tIP Address: {ip.ToString()}");

      System.Console.WriteLine(Constants.TextConstants.GreetingsBody);
      System.Console.WriteLine(Constants.TextConstants.GreetingsOutro);
    }
  }
}
