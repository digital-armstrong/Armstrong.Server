using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class Room
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long BuildingId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();
}
