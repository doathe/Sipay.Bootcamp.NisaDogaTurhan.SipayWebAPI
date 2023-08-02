using AutoMapper;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;

namespace BookStoreWebAPI.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorModel Model { get; set; }

    public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _context.Authors.FirstOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

        // Checks if the author exists
        if (author is not null)
            throw new InvalidOperationException("Author already exist");

        // Maps the CreateAuthorModel to Author
        var newAuthor = _mapper.Map<Author>(Model);

        // Database operations
        _context.Authors.Add(newAuthor);
        _context.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly BirthDate { get; set; }
}