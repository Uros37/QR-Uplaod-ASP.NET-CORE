using System;
using System.Collections.Generic;

namespace QRGEN.Models.Database;

public partial class Favouritesvgfile
{
    public int Id { get; set; }

    public string SvgFileName { get; set; }

    public int UserId { get; set; }

    public int PathNum { get; set; }
}
