using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly BookStoreDbContext _context;

    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }

    public UpdateAuthorCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

        // Checks if the author exists
        if (author is null)
            throw new InvalidOperationException("Author not found");

        if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != AuthorId))
            throw new InvalidOperationException("Author already exist.");

        // Database operations
        author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
        author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
        author.BirthDate = string.IsNullOrEmpty(Model.BirthDate.ToString()) ? author.BirthDate : Model.BirthDate;

        _context.SaveChanges();
    }
}

public class UpdateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
}