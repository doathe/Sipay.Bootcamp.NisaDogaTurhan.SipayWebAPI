using BookStoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebAPI.Context;

/// Migrations = add-migration name
/// Db update = update-database

// Database context class
public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions options) : base(options)
    {

    }

    // DbSets
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Database table configuration applied
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>().HasData(
        new Genre { Id = 1, Name = "Personal Growth", IsActive = true },
        new Genre { Id = 2, Name = "Science Fiction", IsActive = true },
        new Genre { Id = 3, Name = "Romance", IsActive = true }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Lean Startup", PageCount = 200, PublishDate = new DateTime(2001, 06, 12, 0, 0, 0, DateTimeKind.Utc), GenreId = 1 },
            new Book { Id = 2, Title = "Herland", PageCount = 250, PublishDate = new DateTime(2010, 05, 23, 0, 0, 0, DateTimeKind.Utc), GenreId = 2 },
            new Book { Id = 3, Title = "Dune", PageCount = 540, PublishDate = new DateTime(2001, 12, 21, 0, 0, 0, DateTimeKind.Utc), GenreId = 3 }
        );
    }
}
