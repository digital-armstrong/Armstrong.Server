using ArmstrongServer.Helpers;

namespace ArmstrongServer.Models
{
  public class Channel
  {
    public int Id { get; set; }
    public string ChannelName { get; set; }
    public double SystemEventValue { get; set; }
    public double NotSystemEventValue { get; set; }
    public double ImpulsesEventValue { get; set; }
    public DateTime EventDateTime { get; set; }
    public int ChannelState { get; set; }
    public int LingtAllertState { get; set; }
    public int MeasurementType { get; set; } = 1;
    public double ConvertCoefficient { get; set; } = 1;

    public Packages Packages { get; set; }
    public ComPort Port { get; private set; }
    public byte[] ChannelBuffer { get; set; } = new byte[0];

    public Channel(int id, string name, ComPort port)
    {
      Id = id;
      ChannelName = name;
      Packages = PackageHelper.GetPackages((byte)id);
      Port = port;
    }

    public void SendMessage(byte[] message)
    {
      ComPortHelper.SendMessage(Port, message);
    }

    public void ReceiveMessage()
    {
      ChannelBuffer = ComPortHelper.ReadMessage(Port);
    }

    public void SaveEventValue()
    {
      UnitConverterHelper.Convert(this);
      EventDateTime = DateTime.Now;
    }

    public void PrintChannelInfo()
    {
      System.Console.WriteLine($"Name: {this.ChannelName}\t" +
                              $"Impulses: {this.ImpulsesEventValue}\t" +
                              $"System: {this.SystemEventValue.ToString("E3")}\t" +
                              $"NotSyste: {this.NotSystemEventValue.ToString("E3")}\t" +
                              $"Date: {this.EventDateTime}");
    }
  }
}
