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
      var config = SettingsHelper.GetConfiguration();
      var srvAttrConf = config.GetSection("ServerAttributes")
                              .Get<ServerAttributes>();
      var serverAttr = new ServerAttributes
      {
        Id = srvAttrConf.Id,
        Name = srvAttrConf.Name,
      };

      var channels = context
        .Channels.Where(c => c.ServerId == serverAttr.Id)
        .Include(x => x.Device.DeviceModel).Include(x => x.Room).Include(x => x.Device.DeviceModel.MeasurementClass)
        .OrderBy(x => x.ChannelId);

      Console.WriteLine("ID\tSrv\tCh\tName\tLocation\tDeviceType\n");

      foreach (Channel ch in channels)
      {
        ch.Initialization();
        ch.DeviceType = ch.Device.DeviceModel.MeasurementClass.ArmsDeviceType; // It's really wrong choice

        Console.WriteLine($"{ch.Id}\t{ch.ServerId}\t{ch.ChannelId}\t{ch.Name}\t{ch.Room.Name}\t{ch.DeviceType}\n");
      }

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
              EventNotSystemValue = c.EventNotSystemValue,
              EventImpulseValue = c.EventImpulseValue,
              EventDatetime = c.EventDatetime,
              UpdatedAt = c.UpdatedAt,
              CreatedAt = DateTime.UtcNow
            };
            context.Histories.Add(history);
          }
        }

        context.SaveChanges();
      }
    }
  }
}
