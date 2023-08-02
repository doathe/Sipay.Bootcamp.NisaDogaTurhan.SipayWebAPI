using AutoMapper;
using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<AuthorsViewModel> Handle()
    {
        var authors = _context.Authors.OrderBy(x => x.Id).ToList();

        // Maps the Author to AuthorsViewModel
        var viewModel = _mapper.Map<List<AuthorsViewModel>>(authors);

        return viewModel;
    }
}

public class AuthorsViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
}