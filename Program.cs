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
      //   SayHelloWorld.Say();
      //   var context = new DataContext();
      //   var channels = new List<Channel>();
      //   var config = SettingsHelper.GetConfiguration();
      //   var srvAttrConf = config.GetSection("ServerAttributes")
      //                           .Get<ServerAttributes>();
      //   var serverAttr = new ServerAttributes
      //   {
      //     Id = srvAttrConf.Id,
      //     Name = srvAttrConf.Name,
      //   };

      //   channels = context.Channels.Where(c => c.ServerId == serverAttr.Id).ToList<Channel>();
      //   foreach (var c in channels)
      //   {
      //     c.Initialization();
      //   }

      //   while (true)
      //   {
      //     foreach (var c in channels)
      //     {
      //       c.StartOneshotDialogSession();
      //     }

      //     foreach (var c in channels)
      //     {
      //       if (c.ErrorEventCount == 0)
      //       {
      //         var history = new History
      //         {
      //           ChannelId = c.Id,
      //           SystemEventValue = c.SystemEventValue,
      //           EventDate = c.EventDateTime,
      //         };
      //         context.Histories.Add(history);
      //       }
      //     }
      //     context.SaveChanges();
      //   }
    }
  }
}
