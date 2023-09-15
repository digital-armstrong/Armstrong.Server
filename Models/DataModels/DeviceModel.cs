using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class DeviceModel
{
  public long Id { get; set; }

  public string? Name { get; set; }

  public long MeasurementGroupId { get; set; }

  public long MeasurementClassId { get; set; }

  public string? MeasuringUnit { get; set; }

  public string? SafetyClass { get; set; }

  public double? AccuracyClass { get; set; }

  public double? MeasurementSensitivity { get; set; }

  public double? MeasurementMin { get; set; }

  public double? MeasurementMax { get; set; }

  public double? CalibrationMin { get; set; }

  public double? CalibrationMax { get; set; }

  public long ManufacturerId { get; set; }

  public bool? IsCompleteDevice { get; set; }

  public bool? IsTapeRollingMechanism { get; set; }

  public string? DocUrl { get; set; }

  public string? ImageUrl { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

  public virtual Manufacturer Manufacturer { get; set; } = null!;

  public virtual MeasurementClass MeasurementClass { get; set; } = null!;

  public virtual MeasurementGroup MeasurementGroup { get; set; } = null!;
}
