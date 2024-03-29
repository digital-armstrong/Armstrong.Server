﻿using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class Division
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public long OrganizationId { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual Organization Organization { get; set; } = null!;

  public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
