
using ArmstrongServer.Data;
using Microsoft.EntityFrameworkCore;

namespace ArmstrongServer.Models
{
  public class ServerObject : IServer
  {
    public int ServerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ServerName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ComPort ServerComPort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public List<Channel> Channels { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public List<History> Histories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    private bool _isPolling;

    public void Initialization(int serverId)
    {
      ServerId = serverId;
      ServerComPort = new()
      {
        PortName = "",
        BaudRate = 1
      };

      var context = new ArmsWebappDevelopmentContext();
      var channels = context
        .Channels.Where(c => c.ServerId == AppSettings.AppServerAttributes.Id)
        .Include(x => x.Device.DeviceModel).Include(x => x.Room).Include(x => x.Device.DeviceModel.MeasurementClass)
        .OrderBy(x => x.ChannelId);
    }

    public void Start()
    {
      Console.WriteLine($"Thread for Server {ServerId} started.");

      while (_isPolling)
      {
        // Здесь ваш код опроса сервера и обработки данных

        // Пример: Симуляция опроса сервера с задержкой
        Console.WriteLine($"Server {ServerId} -- Work");
        Task.Delay(1000).Wait(); // Пауза между опросами
      }

      Console.WriteLine($"Server {ServerId} -- Reload");
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
