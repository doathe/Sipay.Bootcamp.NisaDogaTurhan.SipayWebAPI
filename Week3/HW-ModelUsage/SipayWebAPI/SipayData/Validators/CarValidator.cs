using FluentValidation;
using SipayData.Entities;

namespace SipayData.Validators;

// Validations added for Car Model with Fluent Validation.
public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        // Brand Required and it's length must be between 3 and 50 characters.
        RuleFor(car => car.Brand)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
            .Length(3, 50).WithMessage("Please ensure you have entered your {PropertyName} between 3 and 50 characters.");

        // Model Required and it's length must be between 3 and 50 characters.
        RuleFor(car => car.Model)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
            .Length(3, 50).WithMessage("Please ensure you have entered your {PropertyName} between 3 and 50 characters.");

        // LicensePlate Required and it's length must be between 3 and 50 characters.
        RuleFor(car => car.LicensePlate)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
            .Length(3, 50).WithMessage("Please ensure you have entered your {PropertyName} between 3 and 50 characters.");

        // Year Required.
        RuleFor(car => car.Year)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}");

        // FuelType Required and must be true type.
        RuleFor(car => car.FuelType)
            .NotNull().NotEmpty().WithMessage("Please ensure you have entered your {PropertyName}")
            .Must(BeValidFuelType).WithMessage("Please enter a valid FuelType ('Diesel', 'Gasoline', 'Electric', etc.).");
    }
    private bool BeValidFuelType(string fuelType)
    {
        // Checks the Fuel Type
        return fuelType == "Diesel" || fuelType == "Gasoline" || fuelType == "Electric" || fuelType == "LPG" || fuelType == "CNG" || fuelType == "Hybrid" || fuelType == "Hydrogen" || fuelType == "Biofuel" || fuelType == "Natural Gas";
    }
}
