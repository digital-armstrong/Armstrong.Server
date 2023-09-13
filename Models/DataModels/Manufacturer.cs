using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class Manufacturer
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public string? Adress { get; set; }

  public string? Phone { get; set; }

  public string? Email { get; set; }

  public string? SiteUrl { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();
}
