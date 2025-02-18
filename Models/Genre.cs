﻿using System;
using System.Collections.Generic;

namespace PKS_Library.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
