using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SipayData.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipayData.Entities;

// Rental entity inherit from BaseEntity class
[Table("Rental", Schema = "dbo")]
public class Rental : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalAmount { get; set; }

    public int CarId { get; set; }
    public Car Car { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}

// Rental Table definitions
public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasIndex(e => e.Id).IsUnique(true);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.StartDate).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.EndDate).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.TotalAmount).IsRequired(true);

        builder.Property(e => e.CreatedOn).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.UpdatedOn).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        // Relations defined
        // 1-M relation between User - Rental
        builder.HasOne(x => x.User)
            .WithMany(x => x.Rentals)
            .HasForeignKey(x => x.UserId);

        // 1-M relation between Car - Rental
        builder.HasOne(x => x.Car)
            .WithMany(x => x.Rentals)
            .HasForeignKey(x => x.CarId);
    }
}
