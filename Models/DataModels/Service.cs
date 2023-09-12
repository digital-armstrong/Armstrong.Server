using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class Service
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public long DivisionId { get; set; }

  public long OrganizationId { get; set; }

  public long BuildingId { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual Building Building { get; set; } = null!;

  public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

  public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

  public virtual Division Division { get; set; } = null!;

  public virtual Organization Organization { get; set; } = null!;

  public virtual ICollection<Server> Servers { get; set; } = new List<Server>();

  public virtual ICollection<User> Users { get; set; } = new List<User>();
}
