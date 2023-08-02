using AutoMapper;
using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly BookStoreDbContext _context;

    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }

    public UpdateGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);

        // Checks if the genre exists
        if (genre == null)
            throw new InvalidOperationException("Genre not found.");

        // Checks if the genre exists
        if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("Genre already exist.");

        // Database operations
        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;

        _context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
