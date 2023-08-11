using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class SupplementaryKit
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? SerialId { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DeviceComponent> DeviceComponents { get; set; } = new List<DeviceComponent>();

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
