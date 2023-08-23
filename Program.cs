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
        .Channels.Where(c => c.ServiceId == serverAttr.Id)
        .Include(x => x.Device.DeviceModel).Include(x => x.Room).Include(x => x.Device.DeviceModel.MeasurementClass)
        .ToList();

      Console.WriteLine("ID\tName\tLocation\tDeviceType\n");

      foreach (Channel ch in channels)
      {
        ch.Initialization();
        ch.DeviceType = ch.Device.DeviceModel.MeasurementClass.ArmsDeviceType; // It's really wrong choice

        Console.WriteLine($"{ch.Id}\t{ch.Name}\t{ch.Room.Name}\t{ch.DeviceType}\n");
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

        // NOTE: If Histories = 0 then app throws exception
        // Cannot write DateTime with Kind=UTC to PostgreSQL type
        // 'timestamp without time zone', consider using 'timestamp with time zone'.
        if (context.Histories.Count() > 0)
          context.SaveChanges();
      }
    }
  }
}
