using AutoMapper;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _context.Set<Book>().FirstOrDefault(x => x.Id == BookId);

        if(book is null)
            throw new InvalidOperationException("Book not found.");

        var viewModel = _mapper.Map<BookDetailViewModel>(book);

        return viewModel;
    }
}

public class BookDetailViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}
