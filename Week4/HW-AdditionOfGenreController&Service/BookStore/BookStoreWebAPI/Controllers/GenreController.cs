using AutoMapper;
using BookStoreWebAPI.Application.GenreOperations.Commands.CreateGenre;
using BookStoreWebAPI.Application.GenreOperations.Commands.DeleteGenre;
using BookStoreWebAPI.Application.GenreOperations.Commands.UpdateGenre;
using BookStoreWebAPI.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreWebAPI.Application.GenreOperations.Queries.GetGenres;
using BookStoreWebAPI.Context;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GenreController(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    // GET: api/<GenreController>s
    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new GetGenresQuery(context, mapper);
        var response = query.Handle();
        return Ok(response);
    }

    // GET api/<GenreController>s/5
    [HttpGet("{id}")]
    public IActionResult GetGenreById(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(context, mapper);
        query.GenreId = id;

        // Validation processes
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var response = query.Handle();

        return Ok(response);
    }

    // POST api/<GenreController>s
    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        CreateGenreCommand command = new CreateGenreCommand(context, mapper);
        command.Model = newGenre;

        // Validation processes
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok(newGenre.Name + " created successfully.");
    }

    // PUT api/<GenreController>s/5
    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(context);
        command.GenreId = id;
        command.Model = updateGenre;

        // Validation processes
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok(updateGenre.Name + " updated successfully.");
    }

    // DELETE api/<GenreController>s/5
    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(context);
        command.GenreId = id;

        // Validation processes
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok("Deleted successfully.");
    }
}
