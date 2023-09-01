using System.IO.Ports;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Models
{
  public class ComPort : SerialPort
  {
    public ComPort()
    {
      PortName = AppSettings.AppPortSettings.PortName;
      BaudRate = AppSettings.AppPortSettings.BaudRate;
    }
  }
}
