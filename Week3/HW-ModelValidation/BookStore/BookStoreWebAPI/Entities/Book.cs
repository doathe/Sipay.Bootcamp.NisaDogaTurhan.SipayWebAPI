using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebAPI.Entities;

// Book entity
[Table("Book", Schema = "BookStore")]
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}

// Book table definition

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasIndex(e => e.Id).IsUnique(true);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.GenreId).IsRequired(true);
        builder.Property(e => e.PageCount).IsRequired(true);
        builder.Property(e => e.PublishDate).IsRequired(true);
    }
}