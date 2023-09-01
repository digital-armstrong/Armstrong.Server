using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class Building
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long OrganizationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
