using System.ComponentModel.DataAnnotations.Schema;
using ArmstrongServer.Constants;
using ArmstrongServer.Helpers;
using ArmstrongServer.Models.ConfigModels;
using ArmstrongServer.Models;

namespace ArmstrongServer.Models.DataModels;

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

  public double SelfBackground { get; set; } = 1;

  public double PreEmergencyLimit { get; set; }

  public double EmergencyLimit { get; set; }

  public double Consumptiom { get; set; }

  public double ConversionCoefficient { get; set; } = 1;

  public double EventSystemValue { get; set; } = 0;

  public double EventNotSystemValue { get; set; } = 0;

  public double EventImpulseValue { get; set; } = 0;

  public DateTime? EventDatetime { get; set; }

  public int? EventCount { get; set; }

  public int? EventErrorCount { get; set; }

  public bool? IsSpecialControl { get; set; }

  public bool IsOnline { get; set; }

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
  public ComPort Port { get; private set; }
  [NotMapped]
  public byte[] ChannelBuffer { get; set; } = new byte[0];
  [NotMapped]
  public int? DeviceType { get; set; }
  [NotMapped]
  public int ErrorCountLimit { get; private set; }
  [NotMapped]
  public int DeadPollingTime { get; set; }

  public void Initialization(ServerProps serverProp)
  {
    this.Packages = PackageHelper.GetPackages((byte)this.ChannelId);
    this.EventCount = 0;
    this.EventErrorCount = 0;
    this.ErrorCountLimit = AppSettings.AppGeneralSettings.ChannelPollingErrorCountLimit;
    this.DeadPollingTime = serverProp.DeadPollingTime;
    this.Port = new ComPort(serverProp);
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
    if (isSaved)
    {
      this.EventCount++;
      this.EventErrorCount = 0;
    }
    else
      this.EventErrorCount++;
  }

  /// <summary>
  /// Get string statements for channel following measurement values
  /// </summary>
  /// <param name="channel">Channel object</param>
  /// <returns>String state value</returns>
  public string SetChannelStatus()
  {
    if (this.IsOnline)
    {
      if (this.EventErrorCount < ErrorCountLimit)
      {
        if (this.EventSystemValue < this.PreEmergencyLimit)
          return ChannelState.Normal;
        else if (this.EventSystemValue >= this.PreEmergencyLimit && this.EventSystemValue < EmergencyLimit)
          return ChannelState.Warning;
        else
          return ChannelState.Danger;
      }
      else
      {
        return ChannelState.LineDown;
      }
    }
    else
    {
      return ChannelState.Offline;
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

      this.EventSystemValue = UnitConverterHelper.ToSystem(this.DeviceType,
                                                           this.ConversionCoefficient,
                                                           this.EventImpulseValue);
      this.EventNotSystemValue = UnitConverterHelper.ToNotSystem(this.DeviceType,
                                                                 this.EventSystemValue);

      this.EventDatetime = DateTime.UtcNow;
      this.UpdatedAt = DateTime.UtcNow;

      return true;
    }
  }

  public void StartOneshotDialogSession()
  {
    this.Port.Open();
    this.SendMessage(this.Packages.Fetch);
    Thread.Sleep(DeadPollingTime);
    this.ReceiveMessage();
    var isSaved = this.SaveEventValue();
    SetEventCount(isSaved);

    this.State = SetChannelStatus();

    if (isSaved)
    {
      Thread.Sleep(DeadPollingTime);
      this.SetLightAlert();
    }

    this.PrintChannelInfo();
    this.Port.Close();
  }

  public void SetLightAlert()
  {
    if (this.IsOnline == PowerState.Off || this.State == ChannelState.LineDown)
      return;

    switch (this.State)
    {
      case ChannelState.Normal:
        SendMessage(this.Packages.LightAlert.Normal);
        break;
      case ChannelState.Warning:
        SendMessage(this.Packages.LightAlert.Warning);
        break;
      case ChannelState.Danger:
        switch (this.IsSpecialControl)
        {
          case false:
            SendMessage(this.Packages.LightAlert.Danger);
            return;
          case true:
            SendMessage(this.Packages.LightAlert.SpecialSignal);
            return;
        }
        break;

    }
  }

  public void PrintChannelInfo()
  {
    System.Console.WriteLine($"Id: {this.ChannelId}\t" +
                            $"Name: {this.Name}\t" +
                            $"Impulses: {this.EventImpulseValue}\t" +
                            $"System: {this.EventSystemValue:E3}\t" +
                            $"NotSyste: {this.EventNotSystemValue:E3}\t" +
                            $"Date: {this.EventDatetime}\t" +
                            $"Count: {this.EventCount}\t" +
                            $"Error: {this.EventErrorCount}\t" +
                            $"State: {this.State}");
  }
}
