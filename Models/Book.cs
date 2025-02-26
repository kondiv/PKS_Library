using System;
using System.Collections.Generic;

namespace PKS_Library.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Isbn { get; set; } = null!;

    public int AuthorId { get; set; }

    public int GenreId { get; set; }

    public int PublishYear { get; set; }

    public int QuantityInStock { get; set; }

    public string Title { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
