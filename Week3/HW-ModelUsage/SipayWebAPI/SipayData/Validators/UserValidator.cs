using FluentValidation;
using SipayData.Entities;

namespace SipayData.Validators;

// Validations added for User Model with Fluent Validation.
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        // FirstName Required and it's length must be between 3 and 50 characters.
        RuleFor(user => user.FirstName)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
            .Length(3, 50).WithMessage("Please ensure you have entered your {PropertyName} between 3 and 50 characters.");

        // LastName Required and it's length must be between 3 and 50 characters.
        RuleFor(user => user.LastName)
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

        // PasswordVerify Required and it must equal to entered password data.
        RuleFor(user => user.PasswordVerify)
            .Equal(user => user.Password).WithMessage("Please ensure your {PropertyName} matches the password.")
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // BirthDate Required and it must greater than or equal to 18 age.
        RuleFor(user => user.BirthDate)
            .Must(birthDate => birthDate <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18))).WithMessage("You must be at least 18 years old.")
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // Phone Required and It must be in correct format.
        RuleFor(user => user.Phone)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
            .Matches(@"^\d{3}\s\d{3}\s\d{2}\s\d{2}$").WithMessage("Please ensure you have entered your {PropertyName} in the correct format.");

        // LicenseNumber Required.
        RuleFor(user => user.LicenseNumber)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

    }
}
