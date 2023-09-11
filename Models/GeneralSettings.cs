namespace ArmstrongServer.Models
{
  public class GeneralSettings
  {
    public string AuthToken { get; set; } = string.Empty;
    public int ChannelPolingTimeout { get; set; }
    public int ChannelPollingErrorCountLimit { get; set; }
    public bool MultiportMode { get; set; }
    public List<MultiportCollection>? MultiportCollection { get; set; }
  }
}
