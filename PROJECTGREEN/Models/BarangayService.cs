using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class BarangayService
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Schedule { get; set; }

    public string? Location { get; set; }

    public string? LocationPhotoPath { get; set; }
}
