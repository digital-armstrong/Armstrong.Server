namespace ArmstrongServer.Models.ConfigModels
{
  public class GeneralSettings
  {
    public string AuthToken { get; set; } = string.Empty;
    public int ChannelPollingErrorCountLimit { get; set; }
    public List<ServerProps>? ServerProps { get; set; }
  }
}
