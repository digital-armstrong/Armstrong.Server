using ArmstrongServer.Models;
using ArmstrongServer.Data;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      SayHelloWorld.Say();

      var config = SettingsHelper.GetConfiguration();
      var srvAttrConf = config.GetSection("ServerAttributes")
                              .Get<ServerAttributes>();
      var serverAttr = new ServerAttributes
      {
        Id = srvAttrConf.Id,
        Name = srvAttrConf.Name,
      };

      List<Channel> channels = new List<Channel>();

      using (var context = new DataContext())
      {
        channels = context.Channels.Where(c => c.ServerId == serverAttr.Id).ToList<Channel>();
        foreach (var c in channels)
        {
          c.Initialization();
        }
      }

      while (true)
      {
        foreach (var c in channels)
        {
          c.Port.Open();
          c.SendMessage(c.Packages.Fetch);
          Thread.Sleep(200);
          c.ReceiveMessage();
          c.SaveEventValue();
          c.PrintChannelInfo();
          Thread.Sleep(1000);
          c.Port.Close();
        }

        using (var context = new DataContext())
        {
          foreach (var c in channels)
          {
            var history = new History
            {
              ChannelId = c.Id,
              SystemEventValue = c.SystemEventValue,
              EventDate = c.EventDateTime,
            };
            context.Histories.Add(history);
          }
          context.SaveChanges();
        }

      }

    }
  }
}
