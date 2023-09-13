using ArmstrongServer.Models.DataModels;
using ArmstrongServer.Models.ConfigModels;
using ArmstrongServer.Data;
using Microsoft.EntityFrameworkCore;
using ArmstrongServer.Interfaces;

namespace ArmstrongServer.Models
{
  public class Server : IServer<ArmsWebappDevelopmentContext>
  {
    public int ServerId { get; set; }
    public int DeadPollingTime { get; set; }
    public int PollingTimeout { get; set; }
    public int MinimalPollingTimeout { get; set; }
    public ComPort ServerComPort { get; set; }
    public IOrderedQueryable<Channel> Channels { get; set; }
    public IOrderedQueryable<History> Histories { get; set; }
    public ArmsWebappDevelopmentContext DataContext { get; set; }

    private bool _isPolling;

    public Server(ServerProps serverProps)
    {
      ServerId = serverProps.ServerId;
      ServerComPort = new(serverProps);
      DeadPollingTime = serverProps.DeadPollingTime;
      PollingTimeout = serverProps.PollingTimeout;
      MinimalPollingTimeout = serverProps.MinimalPollingTimeout;

      DataContext = new();

      Channels = DataContext
        .Channels.Where(c => c.ServerId == this.ServerId)
        .Include(x => x.Device.DeviceModel).Include(x => x.Room).Include(x => x.Device.DeviceModel.MeasurementClass)
        .OrderBy(x => x.ChannelId);

      foreach (var channel in Channels)
        channel.Initialization(serverProps);
    }

    public void Start(CancellationToken cancellationToken)
    {
      _isPolling = true;
      Console.WriteLine($"Thread for Server {ServerId} started.");

      var channelsCount = Channels.Count();
      var summPollingPortOverhead = channelsCount * this.DeadPollingTime * 2;
      var realPollingTimeout = this.PollingTimeout > summPollingPortOverhead
        ? (int)(this.PollingTimeout - summPollingPortOverhead)
        : this.MinimalPollingTimeout;

      while (_isPolling)
      {
        foreach (var c in Channels)
        {
          c.StartOneshotDialogSession();
        }

        foreach (var c in Channels)
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
            DataContext.Histories.Add(history);
          }
        }

        DataContext.SaveChanges();
        Task.Delay(realPollingTimeout).Wait();

        if (cancellationToken.IsCancellationRequested)
          Stop();
        else
          Console.WriteLine($"Server {ServerId}\tWork");
      }

      Console.WriteLine($"Server {ServerId}\tStopped");
    }

    public void Stop()
    {
      _isPolling = false;
    }

    public void Update()
    {
      throw new NotImplementedException();
    }
  }
}
