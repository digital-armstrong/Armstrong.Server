using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArmstrongServer.Helpers;

namespace ArmstrongServer.Models
{
  [Table("channels")]
  public class Channel
  {
    private int _id;

    [Key, Column("id")]
    public int Id
    {
      get { return _id; }
      set { _id = value; Initialization(); System.Console.WriteLine("CALLED!!!"); }
    }
    [Column("channel_id")]
    public int ChannelId { get; set; }
    [Column("server_id")]
    public int ServerId { get; set; }
    [Column("name_controlpoint")]
    public string? ChannelName { get; set; }
    [Column("on_off")]
    public int ChannelPowerState { get; set; }
    [Column("state_for_threeview")]
    public int ChannelState { get; set; }
    [Column("consumption")]
    public double ChannelConsumption { get; set; }
    [Column("special_control")]
    public bool ChannelSpecialControl { get; set; }

    [Column("name_db")]
    public string? DeviceName { get; set; }
    [Column("type")]
    public int DeviceType { get; set; }
    [Column("min_nuclid_value")]
    public double DeviceCalibrateMin { get; set; }
    [Column("max_nuclid_value")]
    public double DeviceCalibrateMax { get; set; }
    [Column("background")]
    public double DeviceSelfBackground { get; set; }
    [Column("name_location")]
    public string? DeviceLocation { get; set; }

    [Column("event_date")]
    public DateTime EventDateTime { get; set; }
    [Column("event_value")]
    public double SystemEventValue { get; set; }
    [Column("unit")]
    public string? Unit { get; set; }
    [NotMapped]
    public double NotSystemEventValue { get; set; }
    [Column("value_impulses")]
    public double ImpulsesEventValue { get; set; }


    [Column("coefficient")]
    public double ConvertCoefficient { get; set; } = 1;
    [Column("pre_accident")]
    public double PreEmgLimit { get; set; }
    [Column("accident")]
    public double EmgLimit { get; set; }
    [Column("count")]
    public int EventCount { get; set; }
    [Column("error_count")]
    public int ErrorEventCount { get; set; }

    [NotMapped]
    public Packages? Packages { get; set; }
    [NotMapped]
    public ComPort Port { get; private set; } = new ComPort();
    [NotMapped]
    public byte[] ChannelBuffer { get; set; } = new byte[0];

    public void Initialization()
    {
      Packages = PackageHelper.GetPackages((byte)this.ChannelId);
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
      EventDateTime = DateTime.UtcNow;
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
