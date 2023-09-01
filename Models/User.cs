using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class User
{
    public long Id { get; set; }

    public int TabelId { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? AvatarUrl { get; set; }

    public long ServiceId { get; set; }

    public string? Email { get; set; }

    public string EncryptedPassword { get; set; } = null!;

    public string? ResetPasswordToken { get; set; }

    public DateTime? ResetPasswordSentAt { get; set; }

    public DateTime? RememberCreatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Timezone { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Inspection> InspectionCreators { get; set; } = new List<Inspection>();

    public virtual ICollection<Inspection> InspectionPerformers { get; set; } = new List<Inspection>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Service Service { get; set; } = null!;
}
