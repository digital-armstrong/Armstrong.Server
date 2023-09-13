namespace ArmstrongServer.Models.ConfigModels
{
  public class ServerProps
  {
    public int ServerId { get; set; }
    public int DeadPollingTime { get; set; }
    public int PollingTimeout { get; set; }
    public int MinimalPollingTimeout { get; set; }
    public string ComPortName { get; set; } = string.Empty;
    public int ComPortBaudRate { get; set; }
  }
}
