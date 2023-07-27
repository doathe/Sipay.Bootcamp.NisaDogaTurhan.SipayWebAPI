using AutoMapper;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.BookOperations.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateBookModel Model { get; set; }

    public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var book = _context.Set<Book>().FirstOrDefault(x => x.Title == Model.Title);

        // Checks if the book exists
        if (book is not null)
            throw new InvalidOperationException("Book already exist");

        // Maps the CreateBookModel to Book
        var newBook = _mapper.Map<Book>(Model);

        // Database operations
        _context.Set<Book>().Add(newBook);
        _context.SaveChanges();
    }
}

public class CreateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}
