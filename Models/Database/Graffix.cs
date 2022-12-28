using System;
using System.Collections.Generic;

namespace QRGEN.Models.Database;

public partial class Graffix
{
    public int Id { get; set; }

    public string MediaId { get; set; }

    public int ArtistId { get; set; }

    public string Title { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Thumbnail { get; set; }

    public string Description { get; set; }

    public string Ourl { get; set; }

    public double? Owidth { get; set; }

    public double? Oheight { get; set; }
}
