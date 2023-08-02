using AutoMapper;
using BookStoreWebAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebAPI.Application.BookOperations.Queries.GetBooks;

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
        var books = _context.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList();

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
    public string Author { get; set; }
}
