using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SipayData.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace SipayData.Entities;

// User entity inherit from BaseEntity class
[Table("User", Schema = "dbo")]
public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordVerify { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; }
    public int LicenseNumber { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Car>? Cars { get; set; }
}

// User Table definitions
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasIndex(e => e.Id).IsUnique(true);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Email).IsRequired(true).HasMaxLength(100);
        builder.Property(e => e.Password).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.PasswordVerify).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.BirthDate).IsRequired(true);
        builder.Property(e => e.Phone).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.LicenseNumber).IsRequired(true);
        builder.Property(e => e.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(e => e.CreatedOn).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.UpdatedOn).IsRequired(true).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}
