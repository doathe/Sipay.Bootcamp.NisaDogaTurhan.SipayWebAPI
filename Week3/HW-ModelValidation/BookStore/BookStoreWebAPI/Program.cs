using BookStoreWebAPI.BookOperations.CreateBook;
using BookStoreWebAPI.Context;
using BookStoreWebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper Dependency Injection
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Configuration Options for db
var connectionString = config.GetConnectionString("PostgreSqlConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string is null or empty.");
}

// Database Dependency Injection
builder.Services.AddDbContext<BookStoreDbContext>(option => option.UseNpgsql(connectionString));

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalLoggingHandler>();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
