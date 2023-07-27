using AutoMapper;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<BooksViewModel> Handle()
    {
        var books = _context.Set<Book>().OrderBy(x => x.Id).ToList<Book>();

        // Maps the Book to BooksViewModel
        var viewModel = _mapper.Map<List<BooksViewModel>>(books);

        return viewModel;
    }
}

public class BooksViewModel
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
}
