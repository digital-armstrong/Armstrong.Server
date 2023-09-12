using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class DeviceComponent
{
  public long Id { get; set; }

  public long? SupplementaryKitId { get; set; }

  public string? SerialId { get; set; }

  public string? Name { get; set; }

  public double? MeasurementMin { get; set; }

  public double? MeasurementMax { get; set; }

  public string? MeasuringUnit { get; set; }

  public string? Description { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual SupplementaryKit? SupplementaryKit { get; set; }
}
