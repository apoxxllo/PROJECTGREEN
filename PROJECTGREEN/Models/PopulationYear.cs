using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class PopulationYear
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public int? Population { get; set; }
}
