namespace ArmstrongServer.Models
{
  public class PortSetting
  {
    public string DefaultPortName { get; } = "/dev/ttyUSB0";
    public string? PortName { get; set; }
    public int BaudRate { get; set; }
  }
}
