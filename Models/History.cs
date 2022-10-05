using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmstrongServer.Models
{
  [Table("histories")]
  public class History
  {
    [Key, Column("channel_id")]
    public int ChannelId { get; set; }
    [Column("event_value")]
    public double SystemEventValue { get; set; }
    [Column("event_date")]
    public DateTime EventDate { get; set; }
  }
}
