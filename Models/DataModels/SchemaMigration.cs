using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models.DataModels;

public partial class SchemaMigration
{
  public string Version { get; set; } = null!;
}
