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

      var cancellationTokenSource = new CancellationTokenSource();
      CancellationToken cancellationToken = cancellationTokenSource.Token;

      var serverTasks = new List<Task>();

      foreach (var server in Servers)
      {
        var task = Task.Run(() => server.Start(cancellationToken), cancellationToken);
        serverTasks.Add(task);
      }

      Console.WriteLine("Starting server tasks...");

      Console.WriteLine("Press Enter to abort polling.");
      Console.ReadLine();

      Console.WriteLine("Stopping server tasks...");

      cancellationTokenSource.Cancel();
      await Task.WhenAll(serverTasks);

      Console.WriteLine("All server tasks have completed.");
    }

    public static void Initialization()
    {
      AppSettings.Initialization();
      Servers = ServerHelper.GetServerList(AppSettings.AppGeneralSettings.ServerProps);
      SayHelloWorld.Say();
    }
  }
}
