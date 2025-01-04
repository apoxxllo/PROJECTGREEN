using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Contact { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ProfilePicPath { get; set; }

    public virtual ICollection<CommitteeAssignment> CommitteeAssignments { get; set; } = new List<CommitteeAssignment>();

    public virtual ICollection<PositionAssignment> PositionAssignments { get; set; } = new List<PositionAssignment>();
}
