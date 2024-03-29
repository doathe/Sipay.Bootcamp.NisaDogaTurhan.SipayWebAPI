﻿using FluentValidation;

namespace BookStoreWebAPI.BookOperations.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.UtcNow.Date);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(3);
    }
}
