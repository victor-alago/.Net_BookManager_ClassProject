using System;
using System.Collections.Generic;

namespace ClassProject.Data;

public partial class BookGenre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
