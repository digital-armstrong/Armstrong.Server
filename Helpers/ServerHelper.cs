using ArmstrongServer.Models;
using ArmstrongServer.Models.ConfigModels;

namespace ArmstrongServer.Helpers;

public static class ServerHelper
{
  public static List<Server> GetServerList(List<ServerProps> serverProps)
  {
    if (serverProps.Count > 0)
    {
      var serverList = new List<Server>();

      foreach (var sp in serverProps)
      {
        serverList.Add(new Server(sp));
      }
      return serverList;
    }
    else
      throw new Exception("Server properties not found");
  }
}
