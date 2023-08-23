using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class History
{
  public long Id { get; set; }

  public long ChannelId { get; set; }

  public double? EventImpulseValue { get; set; }

  public double? EventSystemValue { get; set; }

  public double? EventNotSystemValue { get; set; }

  public DateTime? EventDatetime { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual Channel Channel { get; set; } = null!;
}
