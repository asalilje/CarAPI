using FluentValidation;

namespace Car.API.Entities.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Make).NotEmpty();
            RuleFor(car => car.RentalPricePerDay).GreaterThan(0);
            RuleFor(car => car.CarType).NotEmpty();
            RuleFor(car => car.CarId).NotEmpty();
            RuleFor(car => car.Currency).NotEmpty();
        }
    }
}