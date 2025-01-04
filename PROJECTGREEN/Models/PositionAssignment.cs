using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class PositionAssignment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PositionId { get; set; }

    public virtual Position? Position { get; set; }

    public virtual User? User { get; set; }
}
