using System;
using System.Collections.Generic;

namespace PKS_Library.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string Country { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public string ShortName => $"{LastName} {FirstName[0]}.";

    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        return FullName;
    }
}
