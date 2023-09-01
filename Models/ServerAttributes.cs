using System.Net;

namespace ArmstrongServer.Models
{
  public class ServerAttributes
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public IPAddress[] IPAddress { get; set; }

    public ServerAttributes()
    {
      IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
      this.IPAddress = ipEntry.AddressList;
    }
  }
}
