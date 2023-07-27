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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Database table configuration applied
        modelBuilder.ApplyConfiguration(new BookConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
