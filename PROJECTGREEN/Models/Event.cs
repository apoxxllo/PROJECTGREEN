using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class Event
{
    public int Id { get; set; }

    public string? EventName { get; set; }

    public string? Type { get; set; }

    public int? NoOfParticipants { get; set; }

    public string? Description { get; set; }

    public string? PhotoPath { get; set; }

    public virtual ICollection<ProgramFeedback> ProgramFeedbacks { get; set; } = new List<ProgramFeedback>();
}
