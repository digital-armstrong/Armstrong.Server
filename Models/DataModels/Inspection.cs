using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class Inspection
{
  public long Id { get; set; }

  public long DeviceId { get; set; }

  public string TypeTarget { get; set; } = null!;

  public string State { get; set; } = null!;

  public DateTime? ConclusionDate { get; set; }

  public string? Conclusion { get; set; }

  public string? Description { get; set; }

  public long CreatorId { get; set; }

  public long? PerformerId { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual User Creator { get; set; } = null!;

  public virtual Device Device { get; set; } = null!;

  public virtual User? Performer { get; set; }
}
