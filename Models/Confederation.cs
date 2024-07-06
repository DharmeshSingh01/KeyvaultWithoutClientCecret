using System;
using System.Collections.Generic;

namespace DotNetAPIDS.Models;

public partial class Confederation
{
    public int ConfederationId { get; set; }

    public string? ConfederationName { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
