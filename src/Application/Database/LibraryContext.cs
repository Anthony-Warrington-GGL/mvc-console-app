using Microsoft.EntityFrameworkCore;

namespace mvc_console_app.Models;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }

    public string DbPath { get; }

    public LibraryContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "library.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book
        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id); // Its inferred that PKs are required, so there's no need to state it here

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(b => b.Author)
            .IsRequired();

        // Member
        modelBuilder.Entity<Member>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Member>()
            .Property(m => m.FirstName)
            .IsRequired();

        modelBuilder.Entity<Member>()
            .Property(m => m.LastName)
            .IsRequired();

        // Configure many-to-many relationship between Members and Books
        modelBuilder.Entity<Member>()
            .HasMany // a member has many books
                (m => m.BorrowedBooks); // points to the "navigation property" on Member
            //.WithMany() // and each Book can be associated with many Members
            //.UsingEntity(j => j.ToTable("MemberBooks"));
    }
}