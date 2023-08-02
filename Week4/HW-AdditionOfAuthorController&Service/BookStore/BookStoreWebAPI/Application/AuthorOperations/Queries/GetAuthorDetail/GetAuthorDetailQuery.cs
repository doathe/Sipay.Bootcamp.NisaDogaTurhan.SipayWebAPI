using AutoMapper;
using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int AuthorId { get; set; }

    public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

        // Checks if the author exists
        if (author is null)
            throw new InvalidOperationException("Author not found.");

        // Maps the Author to AuthorDetailViewModel
        var viewModel = _mapper.Map<AuthorDetailViewModel>(author);

        return viewModel;
    }
}

public class AuthorDetailViewModel
{
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
}