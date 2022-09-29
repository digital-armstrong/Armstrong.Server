using System.IO.Ports;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Models
{
  public class ComPort : SerialPort
  {
    public ComPort()
    {
      var config = SettingsHelper.GetConfiguration();
      var portConfig = config.GetSection("PortSettings")
                             .Get<PortSetting>();

      PortName = portConfig.PortName;
      BaudRate = portConfig.BaudRate;

      System.Console.WriteLine($"Com-port Initialized successfully:\n" +
                                $"\tPort Name:\t{this.PortName}\n" +
                                $"\tBaud Rate:\t{this.BaudRate}\n\n");
    }
  }
}
