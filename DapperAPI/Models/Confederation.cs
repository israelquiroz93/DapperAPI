using System;
using System.Collections.Generic;

namespace DapperAPI.Models;

public partial class Confederation
{
    public int ConfederationId { get; set; }

    public string? ConfederationName { get; set; }

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}
