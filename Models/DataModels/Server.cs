using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class Server
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public string? IpAdress { get; set; }

  public int? InventoryId { get; set; }

  public long ServiceId { get; set; }

  public long RoomId { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

  public virtual Room Room { get; set; } = null!;

  public virtual Service Service { get; set; } = null!;
}
