using System;
using System.Collections.Generic;

namespace DbFirstWebApp.Models;

public partial class Divtbl
{
    public string Division { get; set; } = null!;

    public string? Names { get; set; }

    // 부모 Divtbl 자식 Bookstbl
    public virtual ICollection<Bookstbl> Bookstbls { get; set; } = new List<Bookstbl>();
}
