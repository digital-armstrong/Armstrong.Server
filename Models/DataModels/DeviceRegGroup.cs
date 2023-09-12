using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class DeviceRegGroup
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
