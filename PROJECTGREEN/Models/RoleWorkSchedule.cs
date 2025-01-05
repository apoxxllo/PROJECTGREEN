using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class RoleWorkSchedule
{
    public int Id { get; set; }

    public int? EmployeeRoleId { get; set; }

    public string? TaskDescription { get; set; }

    public string? Schedule { get; set; }

    public string? TimeTable { get; set; }

    public string? Remarks { get; set; }

    public virtual EmployeeRole? EmployeeRole { get; set; }
}
