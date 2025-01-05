using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class BarangayPosition
{
    public int Id { get; set; }

    public string? BrgyPosition { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
