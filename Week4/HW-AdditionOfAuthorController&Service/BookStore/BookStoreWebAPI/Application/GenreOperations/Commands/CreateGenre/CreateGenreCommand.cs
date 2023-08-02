using AutoMapper;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreModel Model { get; set; }

    public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Name == Model.Name);

        // Checks if the genre exists
        if (genre is not null)
            throw new InvalidOperationException("Genre already exist.");

        genre = new Genre();
        genre.Name = Model.Name;

        // Database operations
        _context.Genres.Add(genre);
        _context.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}
