using ArmstrongServer.Models;
using ArmstrongServer.Data;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ArmstrongServer
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      SayHelloWorld.Say();

      var context = new ArmsWebappDevelopmentContext();
      var channels = new List<Channel>();

      var config = SettingsHelper.GetConfiguration();
      var srvAttrConf = config.GetSection("ServerAttributes")
                              .Get<ServerAttributes>();
      var serverAttr = new ServerAttributes
      {
        Id = srvAttrConf.Id,
        Name = srvAttrConf.Name,
      };

      channels = context
        .Channels
        .Include(x => x.Device.DeviceModel).Include(x => x.Room).Include(x => x.Device.DeviceModel.MeasurementClass)
        .ToList();

      Console.WriteLine("ID\tName\tLocation\tDeviceType\n");

      foreach (Channel ch in channels)
      {
        ch.Initialization();
        ch.DeviceType = ch.Device.DeviceModel.MeasurementClass.ArmsDeviceType; // It's really wrong choice

        Console.WriteLine($"{ch.Id}\t{ch.Name}\t{ch.Room.Name}\t{ch.DeviceType}\n");
      }

      //   channels = context.Channels.Where(c => c.ServerId == serverAttr.Id).ToList<Channel>();

      while (true)
      {
        foreach (var c in channels)
        {
          c.StartOneshotDialogSession();
        }

        foreach (var c in channels)
        {
          if (c.EventErrorCount == 0)
          {
            var history = new History
            {
              ChannelId = c.Id,
              EventSystemValue = c.EventSystemValue,
              EventDatetime = c.EventDatetime,
            };
            context.Histories.Add(history);
          }
        }
        context.SaveChanges();
      }
    }
  }
}
