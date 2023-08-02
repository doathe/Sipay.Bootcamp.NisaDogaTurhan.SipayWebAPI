﻿using FluentValidation;

namespace BookStoreWebAPI.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
        RuleFor(command => command.Model.Surname).MinimumLength(4).When(x => x.Model.Surname != string.Empty);
        RuleFor(command => command.Model.BirthDate).LessThan(DateOnly.FromDateTime(DateTime.Now)).When(x => x.Model.BirthDate.ToString() != string.Empty);
    }
}
