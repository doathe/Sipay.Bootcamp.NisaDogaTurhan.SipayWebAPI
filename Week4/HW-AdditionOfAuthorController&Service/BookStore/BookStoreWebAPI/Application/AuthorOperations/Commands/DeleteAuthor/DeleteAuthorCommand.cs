using BookStoreWebAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebAPI.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly BookStoreDbContext _context;

    public int AuthorId { get; set; }

    public DeleteAuthorCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == AuthorId);

        // Checks if the author exists
        if (author is null)
            throw new InvalidOperationException("Author not found");

        if (author.Books.Count() > 0)
            throw new InvalidOperationException("Delete author's books first");

        // Database operations
        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}
