using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArmstrongServer.Helpers;
using ArmstrongServer.Constants;

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
    public List<History> Histories { get; set; }

    [NotMapped]
    public Packages? Packages { get; set; }
    [NotMapped]
    public ComPort Port { get; private set; } = new ComPort();
    [NotMapped]
    public byte[] ChannelBuffer { get; set; } = new byte[0];

    public void Initialization()
    {
      this.Packages = PackageHelper.GetPackages((byte)this.ChannelId);
      this.EventCount = 0;
      this.ErrorEventCount = 0;
      this.EventDateTime = EventDateTime = DateTime.UtcNow;
    }

    public void SendMessage(byte[] message)
    {
      ComPortHelper.SendMessage(this.Port, message);
    }

    public void ReceiveMessage()
    {
      ChannelBuffer = ComPortHelper.ReadMessage(this.Port);
    }

    public void SetEventCount(bool isSaved)
    {
      switch (isSaved)
      {
        case true:
          this.EventCount++;
          this.ErrorEventCount = 0;
          break;
        case false:
          this.ErrorEventCount++;
          break;
      }
    }

    public bool SaveEventValue()
    {
      if (this.ChannelBuffer.SequenceEqual(new byte[] { Bytes.CRC_ERROR })
          || this.ChannelBuffer.SequenceEqual(new byte[] { Bytes.SEZE_ERROR }))
        return false;

      var impulses = UnitConverterHelper.ToImpulse(this.ChannelBuffer);

      if (this.ImpulsesEventValue == impulses)
        return false;
      else
      {
        this.ImpulsesEventValue = impulses;

        this.SystemEventValue = UnitConverterHelper.ToSystem(this.DeviceType,
                                                             this.ConvertCoefficient,
                                                             this.ImpulsesEventValue);
        this.NotSystemEventValue = UnitConverterHelper.ToNotSystem(this.DeviceType,
                                                                   this.SystemEventValue);

        EventDateTime = DateTime.UtcNow;

        return true;
      }
    }

    public void StartOneshotDialogSession()
    {
      this.Port.Open();
      this.SendMessage(this.Packages.Fetch);
      Thread.Sleep(100);
      this.ReceiveMessage();
      var isSaved = this.SaveEventValue();
      SetEventCount(isSaved);

      if (isSaved)
      {
        Thread.Sleep(100);
        this.SetLightAlert();
      }

      this.PrintChannelInfo();
      Thread.Sleep(2300);
      this.Port.Close();
    }

    public void SetLightAlert()
    {
      if (this.ChannelPowerState == PowerState.Off)
        return;

      if (this.SystemEventValue < this.PreEmgLimit)
      {
        this.ChannelState = AlertColors.Green;
        SendMessage(this.Packages.LightAlert.Normal);
        return;
      }
      else if (this.SystemEventValue >= this.PreEmgLimit && this.SystemEventValue < this.EmgLimit)
      {
        this.ChannelState = AlertColors.Yellow;
        SendMessage(this.Packages.LightAlert.Warning);
        return;
      }
      else
      {
        this.ChannelState = AlertColors.Red;
        switch (this.ChannelSpecialControl)
        {
          case false:
            SendMessage(this.Packages.LightAlert.Danger);
            return;
          case true:
            SendMessage(this.Packages.LightAlert.SpecialSignal);
            return;
        }
      }
    }

    public void PrintChannelInfo()
    {
      System.Console.WriteLine($"Name: {this.ChannelName}\t" +
                              $"Impulses: {this.ImpulsesEventValue}\t" +
                              $"System: {this.SystemEventValue.ToString("E3")}\t" +
                              $"NotSyste: {this.NotSystemEventValue.ToString("E3")}\t" +
                              $"Date: {this.EventDateTime}\t" +
                              $"Count: {this.EventCount}\t" +
                              $"Error: {this.ErrorEventCount}");
    }
  }
}
