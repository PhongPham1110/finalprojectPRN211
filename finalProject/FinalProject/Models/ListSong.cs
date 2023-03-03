using System;
using System.Collections.Generic;

namespace Models;

public partial class ListSong
{
    public int ListNumber { get; set; }

    public int SongNumber { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual List ListNumberNavigation { get; set; }

    public virtual Song SongNumberNavigation { get; set; }
}
