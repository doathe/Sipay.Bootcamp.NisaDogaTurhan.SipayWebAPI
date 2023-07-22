using FluentValidation;
using SipayData.Entities;

namespace SipayData.Validators;

// Validations added for Rental Model with Fluent Validation.
public class RentalValidator : AbstractValidator<Rental>
{
    public RentalValidator()
    {
        // StartDate Required.
        RuleFor(rental => rental.StartDate)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // EndDate Required.
        RuleFor(rental => rental.EndDate)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // TotalAmount Required.
        RuleFor(rental => rental.TotalAmount)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // CarId Required.
        RuleFor(rental => rental.CarId)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // UserId Required.
        RuleFor(rental => rental.UserId)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}