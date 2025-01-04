using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class Position
{
    public int Id { get; set; }

    public string? PositionName { get; set; }

    public string? Responsibilities { get; set; }

    public virtual ICollection<PositionAssignment> PositionAssignments { get; set; } = new List<PositionAssignment>();
}
