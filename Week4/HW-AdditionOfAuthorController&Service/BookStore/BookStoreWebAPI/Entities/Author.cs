using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebAPI.Entities;

// Author entity
[Table("Author", Schema = "BookStore")]
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }

    public virtual List<Book>? Books { get; set;}
}
// Author table definition
public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasIndex(e => e.Id).IsUnique(true);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Surname).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.BirthDate).IsRequired(true);

        builder.HasMany(x => x.Books)
               .WithOne(x => x.Author)
               .HasForeignKey(e => e.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}