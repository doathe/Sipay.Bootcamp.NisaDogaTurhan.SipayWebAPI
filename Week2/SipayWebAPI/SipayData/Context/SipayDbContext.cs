using Microsoft.EntityFrameworkCore;
using SipayData.Entities;

namespace SipayData.Context;

/// Migrations = add-migration name
/// Db update = update-database

// Database context class
public class SipayDbContext : DbContext
{
    public SipayDbContext(DbContextOptions options) : base(options)
    {

    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Database table configurations applied
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new RentalConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}