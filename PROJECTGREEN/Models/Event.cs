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

    public string? Location { get; set; }

    public string? DocuLink { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<ProgramFeedback> ProgramFeedbacks { get; set; } = new List<ProgramFeedback>();
}
