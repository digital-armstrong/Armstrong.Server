using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class Organization
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? FullAddress { get; set; }

    public string? ZipCode { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
