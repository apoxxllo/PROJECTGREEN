using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class Incident
{
    public int Id { get; set; }

    public string? Location { get; set; }

    public DateTime? Date { get; set; }

    public string? IncidentType { get; set; }

    public string? Reason { get; set; }

    public string? Resolution { get; set; }

    public string? DocumentationLink { get; set; }

    public string? Status { get; set; }
}
