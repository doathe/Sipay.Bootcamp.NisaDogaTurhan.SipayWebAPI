using FluentValidation;

namespace BookStoreWebAPI.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(3);
        RuleFor(command => command.Model.BirthDate).NotEmpty().LessThan(DateOnly.FromDateTime(DateTime.Now));
    }
}
