namespace ArmstrongServer.Models
{
  public class PortSettings
  {
    public string DefaultPortName { get; } = "/dev/ttyUSB0";
    public string PortName { get; set; } = "/dev/ttyUSB0";
    public int BaudRate { get; set; }
    public int PollingTime { get; set; }
    public int DeadPollingTime { get; set; }
    public int MinimalPollingTimeout { get; set; }
  }
}
