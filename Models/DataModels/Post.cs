using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class Post
{
  public long Id { get; set; }

  public long UserId { get; set; }

  public string? Title { get; set; }

  public string? Body { get; set; }

  public string? Category { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public virtual User User { get; set; } = null!;
}
