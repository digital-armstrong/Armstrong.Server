using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class MeasurementClass
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public long MeasurementGroupId { get; set; }

  public int? ArmsDeviceType { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();

  public virtual MeasurementGroup MeasurementGroup { get; set; } = null!;
}
