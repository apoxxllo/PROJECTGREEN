using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class ReportGarbageCollection
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Purok { get; set; }

    public string? Address { get; set; }

    public string? Lat { get; set; }

    public DateTime? Date { get; set; }

    public string? Status { get; set; }
}
