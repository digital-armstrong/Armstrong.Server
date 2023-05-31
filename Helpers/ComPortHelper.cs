using ArmstrongServer.Constants;
using ArmstrongServer.Models;
using ArmstrongServer.Helpers;

namespace ArmstrongServer.Helpers
{
  public static class ComPortHelper
  {
    public static void SendMessage(ComPort port, byte[] message)
    {
      if (!port.IsOpen)
      {
        port.Open();
        System.Console.WriteLine("Com-port: Port be open.");
      }

      port.Write(message, 0, 8);
    }

    public static byte[] ReadMessage(ComPort port)
    {
      var packageSize = port.BytesToRead;
      var bufferSize = 8;
      var buffer = new byte[bufferSize];

      if (packageSize != bufferSize)
      {
        System.Console.WriteLine($"SIZE ERROR: Package size = {packageSize}");
        return new byte[] { Bytes.SEZE_ERROR };
      }

      for (var i = 0; i < packageSize; i++)
      {
        buffer[i] = (byte)port.ReadByte();
      }

      if (VerifivationPackageHelper.IsVerified(buffer))
        return buffer;
      else
      {
        System.Console.WriteLine("CRC ERROR");
        return new byte[] { Bytes.CRC_ERROR };
      }
    }
  }
}
