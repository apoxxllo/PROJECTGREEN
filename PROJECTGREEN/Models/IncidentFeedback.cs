using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class IncidentFeedback
{
    public int Id { get; set; }

    public int? IncidentId { get; set; }

    public string? Comment { get; set; }

    public DateTime? Date { get; set; }

    public virtual Incident? Incident { get; set; }
}
