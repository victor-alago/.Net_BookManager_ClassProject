using System;
using System.Collections.Generic;
using ClassProject.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassProject.Data.DbContext;

public partial class BookDataContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BookDataContext()
    {
    }

    public BookDataContext(DbContextOptions<BookDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookGenre> BookGenres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:msdotnet.database.windows.net,1433;Initial Catalog=BookData;Persist Security Info=False;User ID=victor;Password=epita@2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07C257E030");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA166678F4").IsUnique();

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(25);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Genre");
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookGenr__3214EC0789D11D4D");

            entity.ToTable("BookGenre");

            entity.HasIndex(e => e.Name, "UQ__BookGenr__737584F698088CF0").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
