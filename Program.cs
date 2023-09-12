using ArmstrongServer.Models.ConfigModels;
using ArmstrongServer.Models;
using ArmstrongServer.Helpers;

namespace ArmstrongServer
{
  public static class Program
  {
    public static List<Server> Servers { get; set; }
    public static async Task Main(string[] args)
    {
      Initialization();

      var serverThreads = new List<Thread>();

      foreach (var server in Servers)
      {
        var thread = new Thread(() => server.Start());
        serverThreads.Add(thread);
        thread.Start();
      }

      Console.WriteLine("Starting server threads...");

      Console.WriteLine("Press Enter to exit.");
      Console.ReadLine();

      Console.WriteLine("Stopping server threads...");

      // this is not supported by the netcore 6, use concellationToken
      foreach (var thread in serverThreads)
      {
        thread.Abort();
      }

      await Task.WhenAll(serverThreads.Select(thread => Task.Run(() => thread.Join())).ToArray());

      Console.WriteLine("All server threads have completed.");
    }

    public static void Initialization()
    {
      AppSettings.Initialization();
      Servers = ServerHelper.GetServerList(AppSettings.AppGeneralSettings.ServerProps);
      SayHelloWorld.Say();
    }
  }
}
