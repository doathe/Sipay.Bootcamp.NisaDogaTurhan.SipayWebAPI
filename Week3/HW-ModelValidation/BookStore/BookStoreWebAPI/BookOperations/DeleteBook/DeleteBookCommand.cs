using BookStoreWebAPI.BookOperations.UpdateBook;
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

        if (book is null)
            throw new InvalidOperationException("Book not found");

        _context.Set<Book>().Remove(book);
        _context.SaveChanges();
    }
}
