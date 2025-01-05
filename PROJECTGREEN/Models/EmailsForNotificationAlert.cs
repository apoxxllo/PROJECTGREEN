using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class EmailsForNotificationAlert
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Zone { get; set; }

    public string? DayOfTheWeek { get; set; }
}
