using System;
using System.Collections.Generic;

namespace PROJECTGREEN.Models;

public partial class CommitteeAssignment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CommitteeId { get; set; }

    public virtual Committee? Committee { get; set; }

    public virtual User? User { get; set; }
}
