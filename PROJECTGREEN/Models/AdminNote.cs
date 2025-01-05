using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class AdminNote
{
    public int Id { get; set; }

    public string? Note { get; set; }

    public DateTime? Date { get; set; }

    public string? Subject { get; set; }
}
