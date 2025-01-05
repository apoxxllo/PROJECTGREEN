using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class EmployeeRole
{
    public int Id { get; set; }

    public string? EmployeeRole1 { get; set; }

    public virtual ICollection<RoleWorkSchedule> RoleWorkSchedules { get; set; } = new List<RoleWorkSchedule>();
}
