using System;
using System.Collections.Generic;

namespace ArmstrongServer.Models;

public partial class SchemaMigration
{
    public string Version { get; set; } = null!;
}
