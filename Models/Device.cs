using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class Device
{
    public long Id { get; set; }

    public int? InventoryId { get; set; }

    public string? SerialId { get; set; }

    public int? TabelId { get; set; }

    public long DeviceModelId { get; set; }

    public long DeviceRegGroupId { get; set; }

    public int? YearOfProduction { get; set; }

    public int? YearOfCommissioning { get; set; }

    public long? SupplementaryKitId { get; set; }

    public long ServiceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long? RoomId { get; set; }

    public string InspectionExpirationStatus { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

    public virtual DeviceModel DeviceModel { get; set; } = null!;

    public virtual DeviceRegGroup DeviceRegGroup { get; set; } = null!;

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

    public virtual Room? Room { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual SupplementaryKit? SupplementaryKit { get; set; }
}
