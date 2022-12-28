using System;
using System.Collections.Generic;

namespace QRGEN.Models.Database;

public partial class Artist
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
