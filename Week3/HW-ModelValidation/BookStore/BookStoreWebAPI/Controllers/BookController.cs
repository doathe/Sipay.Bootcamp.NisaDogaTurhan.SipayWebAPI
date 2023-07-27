using AutoMapper;
using BookStoreWebAPI.BookOperations.CreateBook;
using BookStoreWebAPI.BookOperations.DeleteBook;
using BookStoreWebAPI.BookOperations.GetBookDetail;
using BookStoreWebAPI.BookOperations.GetBooks;
using BookStoreWebAPI.BookOperations.UpdateBook;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreWebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    // GET: api/<BookController>s
    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(context, mapper);
        var response = query.Handle();
        return Ok(response);
    }

    // GET api/<BookController>s/5
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
        query.BookId = id;

        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var validationResult = validator.Validate(query);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(string.Join(", ", errorMessages));
        }

        var response = query.Handle();

        return Ok(response);
    }

    // POST api/<BookController>s
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(context, mapper);
        command.Model = newBook;

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(string.Join(", ", errorMessages));
        }

        command.Handle();

        return Ok(newBook.Title + " created successfully.");
    }

    // PUT api/<BookController>s/5
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(context);
        command.BookId = id;
        command.Model = updateBook;

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(string.Join(", ", errorMessages));
        }

        command.Handle();

        return Ok(updateBook.Title + " updated successfully.");
    }

    // DELETE api/<BookController>s/5
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new DeleteBookCommand(context);
        command.BookId = id;

        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
            return BadRequest(string.Join(", ", errorMessages));
        }

        command.Handle();

        return Ok("Deleted successfully.");
    }
}
