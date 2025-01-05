using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class ProgramFeedback
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public string? Comment { get; set; }

    public int? Stars { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual Event? Event { get; set; }
}
