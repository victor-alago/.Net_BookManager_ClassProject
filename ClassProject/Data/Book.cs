using System;
using System.Collections.Generic;

namespace ClassProject.Data;

public partial class Book
{
    public int Id { get; set; }

    public string Isbn { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int NumberOfPages { get; set; }

    public string? Author { get; set; }

    public DateOnly PublicationDate { get; set; }

    public int GenreId { get; set; }

    public virtual BookGenre Genre { get; set; } = null!;
}
