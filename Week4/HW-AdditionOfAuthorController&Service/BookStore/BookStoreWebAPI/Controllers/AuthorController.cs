using AutoMapper;
using BookStoreWebAPI.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebAPI.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStoreWebAPI.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStoreWebAPI.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebAPI.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebAPI.Context;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public AuthorController(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    // GET: api/<AuthorController>s
    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new GetAuthorsQuery(context, mapper);
        var response = query.Handle();
        return Ok(response);
    }

    // GET api/<AuthorController>s/5
    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(context, mapper);
        query.AuthorId = id;

        // Validation processes
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var response = query.Handle();

        return Ok(response);
    }

    // POST api/<AuthorController>s
    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        command.Model = newAuthor;

        // Validation processes
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok(newAuthor.Name + " " + newAuthor.Surname + " created successfully.");
    }

    // PUT api/<AuthorController>s/5
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(context);
        command.AuthorId = id;
        command.Model = updateAuthor;

        // Validation processes
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok(updateAuthor.Name + " " + updateAuthor.Surname + " updated successfully.");
    }

    // DELETE api/<AuthorController>s/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(context);
        command.AuthorId = id;

        // Validation processes
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok("Deleted successfully.");
    }
}
