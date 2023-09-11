namespace ArmstrongServer.Models
{
  interface IServer
  {
    public int ServerId { get; set; }
    public string ServerName { get; set; }
    public ComPort ServerComPort { get; set; }
    public List<Channel> Channels { get; set; }
    public List<History> Histories { get; set; }

    public void Initialization(int serverId);
    public void Start();
    public void Stop();
    public void Update();
  }
}
