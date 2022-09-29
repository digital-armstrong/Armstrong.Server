namespace ArmstrongServer.Models
{
  public class WorkMode
  {
    public byte[]? SilenMode { get; set; }
    public byte[]? FrequencyMode { get; set; }
    public byte[]? TimingMode { get; set; }
    public byte[]? GetMode { get; set; }
  }

  public class Service
  {
    public byte[]? OpenBlanker { get; set; }
    public byte[]? CloseBlanker { get; set; }
    public byte[]? StartRewind { get; set; }
    public byte[]? RewindAndBlanker { get; set; }
    public byte[]? GetResult { get; set; }
  }

  public class LightAlert
  {
    public byte[]? Normal { get; set; }
    public byte[]? Warning { get; set; }
    public byte[]? Danger { get; set; }
    public byte[]? SpecialSignal { get; set; }
  }

  public class Packages
  {
    public byte[]? Fetch { get; set; }
    public WorkMode? WorkMode { get; set; }
    public Service? Service { get; set; }
    public LightAlert? LightAlert { get; set; }
    public byte[]? SetAddress { get; set; }
  }
}
