using ArmstrongServer.Models.DataModels;
using ArmstrongServer.Models.ConfigModels;
using ArmstrongServer.Data;
using ArmstrongServer.Models;

namespace ArmstrongServer.Interfaces;
interface IServer<TDataContext> where TDataContext : class
{
  public int ServerId { get; set; }
  public int DeadPollingTime { get; set; }
  public int PollingTimeout { get; set; }
  public int MinimalPollingTimeout { get; set; }
  public ComPort ServerComPort { get; set; }
  public TDataContext DataContext { get; set; }
  public IOrderedQueryable<Channel> Channels { get; set; }
  public IOrderedQueryable<History> Histories { get; set; }

  public void Start(CancellationToken cancellationToken);
  public void Stop();
  public void Update();
}

