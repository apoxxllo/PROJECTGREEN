using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class EmployeeTimetable
{
    public int Id { get; set; }

    public string? Time { get; set; }

    public string? MondayTask { get; set; }

    public string? TuesTask { get; set; }

    public string? WedTask { get; set; }

    public string? ThuTask { get; set; }

    public string? FriTask { get; set; }
}
