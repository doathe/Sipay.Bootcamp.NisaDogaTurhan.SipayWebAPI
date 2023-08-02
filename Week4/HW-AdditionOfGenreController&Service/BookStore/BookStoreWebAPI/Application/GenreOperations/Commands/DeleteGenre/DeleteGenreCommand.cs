using BookStoreWebAPI.Context;

namespace BookStoreWebAPI.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    private readonly BookStoreDbContext _context;

    public int GenreId { get; set; }

    public DeleteGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);

        // Checks if the genre exists
        if (genre == null)
            throw new InvalidOperationException("Genre not found.");

        // Database operations
        _context.Genres.Remove(genre);
        _context.SaveChanges();
    }
}
