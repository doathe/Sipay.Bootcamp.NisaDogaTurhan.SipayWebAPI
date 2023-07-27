using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext _context;
    public int BookId { get; set; }

    public DeleteBookCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var book = _context.Set<Book>().FirstOrDefault(x => x.Id == BookId);

        // Checks if the book exists
        if (book is null)
            throw new InvalidOperationException("Book not found");

        // Database operations
        _context.Set<Book>().Remove(book);
        _context.SaveChanges();
    }
}
