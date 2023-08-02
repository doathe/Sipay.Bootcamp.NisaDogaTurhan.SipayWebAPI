using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebAPI.Entities;

// Genre entity
[Table("Genre", Schema = "BookStore")]
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Book>? Books { get; set; }
}
// 9.dk değerlere bak
// Genre table definition
public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasIndex(e => e.Id).IsUnique(true);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.HasMany(x => x.Books)
               .WithOne(x => x.Genre)
               .HasForeignKey(e => e.GenreId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}