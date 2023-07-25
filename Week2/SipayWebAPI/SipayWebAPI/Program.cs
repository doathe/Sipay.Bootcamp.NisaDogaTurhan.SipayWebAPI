using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SipayData.Context;
using SipayData.Validators;
using SipayWebAPI.Middlewares;
using SipayWebAPI.Services.CarService;
using SipayWebAPI.Services.RentalService;
using SipayWebAPI.Services.UserService;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Fluent Validation added.
builder.Services.AddControllers().AddFluentValidation(configuration =>{configuration.RegisterValidatorsFromAssemblyContaining<UserValidator>();});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration Options for db
var connectionString = config.GetConnectionString("PostgreSqlConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string is null or empty.");
}

// Database Dependency Injection
builder.Services.AddDbContext<SipayDbContext>(option => option.UseNpgsql(connectionString));

// Service Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();

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
