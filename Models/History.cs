using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmstrongServer.Models
{
  [Table("histories")]
  public class History
  {
    [Key, Column("id")]
    public long Id { get; set; }
    [Column("channel_id")]
    public int ChannelId { get; set; }
    public Channel Channel { get; set; }
    [Column("event_value")]
    public double SystemEventValue { get; set; }
    [Column("event_date")]
    public DateTime EventDate { get; set; }
  }
}
