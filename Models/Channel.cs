using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class Channel
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public int? ChannelId { get; set; }

  public long DeviceId { get; set; }

  public long RoomId { get; set; }

  public long ServerId { get; set; }

  public long ServiceId { get; set; }

  public string? LocationDescription { get; set; }

  public double? SelfBackground { get; set; }

  public double? PreEmergencyLimit { get; set; }

  public double? EmergencyLimit { get; set; }

  public double? Consumptiom { get; set; }

  public double? ConversionCoefficient { get; set; }

  public double? EventSystemValue { get; set; }

  public double? EventNotSystemValue { get; set; }

  public double? EventImpulseValue { get; set; }

  public DateTime? EventDatetime { get; set; }

  public int? EventCount { get; set; }

  public int? EventErrorCount { get; set; }

  public bool? IsSpecialControl { get; set; }

  public bool? IsOnline { get; set; }

  public string? State { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual Device Device { get; set; } = null!;

  public virtual ICollection<History> Histories { get; set; } = new List<History>();

  public virtual Room Room { get; set; } = null!;

  public virtual Server Server { get; set; } = null!;

  public virtual Service Service { get; set; } = null!;

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
    this.EventErrorCount = 0;
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
        this.EventErrorCount = 0;
        break;
      case false:
        this.EventErrorCount++;
        break;
    }
  }

  public bool SaveEventValue()
  {
    if (this.ChannelBuffer.SequenceEqual(new byte[] { Bytes.CRC_ERROR })
        || this.ChannelBuffer.SequenceEqual(new byte[] { Bytes.SEZE_ERROR }))
      return false;

    var impulses = UnitConverterHelper.ToImpulse(this.ChannelBuffer);

    if (this.EventImpulseValue == impulses)
      return false;
    else
    {
      this.EventImpulseValue = impulses;

      this.SystemEventValue = UnitConverterHelper.ToSystem(this.DeviceType,
                                                           this.ConversionCoefficient,
                                                           this.EventImpulseValue);
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
