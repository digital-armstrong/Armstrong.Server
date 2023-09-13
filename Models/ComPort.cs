using System.IO.Ports;
using ArmstrongServer.Models.ConfigModels;

namespace ArmstrongServer.Models
{
  public class ComPort : SerialPort
  {
    public ComPort(ServerProps serverProps)
    {
      PortName = serverProps is null ? throw new ArgumentNullException("serverProps") : serverProps.ComPortName;
      BaudRate = serverProps.ComPortBaudRate;
    }
  }
}
