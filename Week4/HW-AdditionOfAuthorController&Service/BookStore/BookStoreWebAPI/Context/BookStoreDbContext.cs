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
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Database table configuration applied
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>().HasData(
        new Genre { Id = 1, Name = "Personal Growth", IsActive = true },
        new Genre { Id = 2, Name = "Science Fiction", IsActive = true },
        new Genre { Id = 3, Name = "Romance", IsActive = true }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Lean Startup", PageCount = 200, PublishDate = new DateTime(2001, 06, 12, 0, 0, 0, DateTimeKind.Utc), GenreId = 1, AuthorId = 1},
            new Book { Id = 2, Title = "Herland", PageCount = 250, PublishDate = new DateTime(2010, 05, 23, 0, 0, 0, DateTimeKind.Utc), GenreId = 2, AuthorId = 2},
            new Book { Id = 3, Title = "Dune", PageCount = 540, PublishDate = new DateTime(2001, 12, 21, 0, 0, 0, DateTimeKind.Utc), GenreId = 3, AuthorId = 3}
        );

        modelBuilder.Entity<Author>().HasData(
        new Author { Id = 1, Name = "Eric", Surname = "Ries", BirthDate = new DateOnly(1960, 04, 20)},
        new Author { Id = 2, Name = " Charlotte Perkins", Surname = "Gilman", BirthDate = new DateOnly(1970, 04, 20) },
        new Author { Id = 3, Name = "Frank", Surname = "Herbert", BirthDate = new DateOnly(1980, 04, 20) }
        );
    }
}
