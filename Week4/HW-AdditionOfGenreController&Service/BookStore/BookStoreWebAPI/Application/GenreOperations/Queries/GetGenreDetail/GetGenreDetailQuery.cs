using AutoMapper;
using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public int GenreId { get; set; }

    public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId && x.IsActive);

        // Checks if the genre exists
        if (genre == null)
            throw new InvalidOperationException("Genre not found.");

        // Maps the Genre to GenreDetailViewModel
        var responseModel = _mapper.Map<GenreDetailViewModel>(genre);

        return responseModel;
    }
}
public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}