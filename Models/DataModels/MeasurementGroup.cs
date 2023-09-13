using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class MeasurementGroup
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();

  public virtual ICollection<MeasurementClass> MeasurementClasses { get; set; } = new List<MeasurementClass>();
}
