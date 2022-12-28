using System;
using System.Collections.Generic;

namespace QRGEN.Models.Database;

public partial class FavouriteWarpText
{
    public int Id { get; set; }

    public int FontId { get; set; }

    public string SvgFileName { get; set; }
}
