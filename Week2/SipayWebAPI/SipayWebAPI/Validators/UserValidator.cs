using FluentValidation;
using SipayWebAPI.Entities;

namespace SipayWebAPI.Validators
{
    // Validations added for User Model with Fluent Validation.
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            // Name Required and it's length must be between 3 and 50 characters.
            RuleFor(user => user.Name)
                .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
                .Length(3, 50).WithMessage("Please ensure you have entered your {PropertyName} between 3 and 50 characters.");

            // Lastname Required and it's length must be between 3 and 50 characters.
            RuleFor(user => user.Lastname)
                .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
                .Length(3, 50).WithMessage("Please ensure you have entered your {PropertyName} between 3 and 50 characters.");

            // Email Required and it must be in Email Format.
            RuleFor(user => user.Email)
                .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
                .EmailAddress().WithMessage("Please ensure you have entered your {PropertyName} in the correct format.");

            // Password Required and it's length must be greater than 7. It must contain at least one uppercase letter and one digit.
            RuleFor(user => user.Password)
                .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
                .MinimumLength(7).WithMessage("Please ensure you have entered your {PropertyName} more than 7 characters.")
                .Matches(@"^(?=.*[A-Z])(?=.*\d).+$").WithMessage("Please ensure you have entered your {PropertyName} at least one uppercase letter and one digit.");

            // Age Required and it must greater than or equal to 18.
            RuleFor(user => user.Age)
                .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
                .GreaterThanOrEqualTo(18).WithMessage("Please ensure you have entered your {PropertyName} greater than or equal to 18+.");
        }
    }
}
