using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.BookOperations.Commands.DeleteBook;

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
        var book = _context.Books.FirstOrDefault(x => x.Id == BookId);

        // Checks if the book exists
        if (book is null)
            throw new InvalidOperationException("Book not found");

        // Database operations
        _context.Books.Remove(book);
        _context.SaveChanges();
    }
}
