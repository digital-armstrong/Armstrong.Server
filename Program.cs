using ArmstrongServer.Models;
using ArmstrongServer.Data;
using ArmstrongServer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ArmstrongServer
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      AppSettings.Initialization();

      SayHelloWorld.Say();

      var context = new ArmsWebappDevelopmentContext();

      var channels = context
        .Channels.Where(c => c.ServerId == AppSettings.AppServerAttributes.Id)
        .Include(x => x.Device.DeviceModel).Include(x => x.Room).Include(x => x.Device.DeviceModel.MeasurementClass)
        .OrderBy(x => x.ChannelId);

      var channelsCount = channels.Count();
      var summPollingPortOverhead = channelsCount * AppSettings.AppPortSettings.DeadPollingTime * 2;
      var realPollingTimeout = AppSettings.AppGeneralSettings.ChannelPolingTimeout > summPollingPortOverhead
        ? (int)(AppSettings.AppGeneralSettings.ChannelPolingTimeout - summPollingPortOverhead)
        : AppSettings.AppPortSettings.MinimalPollingTimeout;

      foreach (Channel ch in channels)
      {
        ch.Initialization();
        ch.DeviceType = ch.Device.DeviceModel.MeasurementClass.ArmsDeviceType; // It's really wrong choice
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
        Thread.Sleep(realPollingTimeout);
      }
    }
  }
}
