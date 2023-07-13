using FluentValidation.AspNetCore;
using SipayWebAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Fluent Validation added.
builder.Services.AddControllers().AddFluentValidation(configuration =>
    {
        configuration.RegisterValidatorsFromAssemblyContaining<UserValidator>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
