using AutoMapper;
using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres = _context.Genres.Where(x => x.IsActive == true).OrderBy(x => x.Id).ToList();

        // Maps the Genre to GenresViewModel
        var responseModel = _mapper.Map<List<GenresViewModel>>(genres);

        return responseModel;
    }
}

public class GenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}