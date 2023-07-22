using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SipayData.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipayData.Entities;

// Car entity inherit from BaseEntity class
[Table("Car", Schema = "dbo")]
public class Car : BaseEntity
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public int Year { get; set; }
    public string FuelType { get; set; }
    public decimal DailyPrice { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Rental>? Rentals { get; set; }
}

// Car Table definitions
public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasIndex(e => e.Id).IsUnique(true);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Brand).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Model).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.LicensePlate).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Year).IsRequired(true);
        builder.Property(e => e.FuelType).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.DailyPrice).IsRequired(true);
        builder.Property(e => e.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(e => e.CreatedOn).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.UpdatedOn).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}
