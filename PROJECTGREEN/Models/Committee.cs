using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class Committee
{
    public int Id { get; set; }

    public string? CommitteeName { get; set; }

    public string? Responsibilities { get; set; }

    public string? RoleInGreenInitiatives { get; set; }

    public virtual ICollection<CommitteeAssignment> CommitteeAssignments { get; set; } = new List<CommitteeAssignment>();
}
