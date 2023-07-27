using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _context;
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }

    public UpdateBookCommand(BookStoreDbContext context)
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
        book.Title = Model.Title != default ? Model.Title : book.Title;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

        _context.SaveChanges();
    }
}

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
}