using FluentValidation;
using UserWebApp.Models;

namespace UserWebApp.Validators
{
    public class RestaurantValidator : AbstractValidator<Restaurant>
    {
        public RestaurantValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("The Name field is required.");

            RuleFor(s => s.City)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The City field is required.");

            //Validate Point Coordinates
            RuleFor(s => s.Location.Coordinate.CoordinateValue.X)
                .NotEmpty()
                .LessThanOrEqualTo(32.123456)
                .GreaterThanOrEqualTo(30.123456)
                .WithMessage("The Point Coordinate value is required and must be within the specified range.");
            RuleFor(s => s.Location.Coordinate.CoordinateValue.Y)
                .NotEmpty()
                .LessThanOrEqualTo(36.123456)
                .GreaterThanOrEqualTo(34.123456)
                .WithMessage("The Point Coordinate value is required and must be within the specified range.");

            //Validate Polygon Coordinates
            RuleFor(s => s.DeliveryAreaCoverage.Coordinate.CoordinateValue.X)
                .NotEmpty()
                .LessThanOrEqualTo(32.123456)
                .GreaterThanOrEqualTo(30.123456)
                .WithMessage("The Polygon Coordinate value is required and must be within the specified range.");
            RuleFor(s => s.DeliveryAreaCoverage.Coordinate.CoordinateValue.Y)
                .NotEmpty()
                .LessThanOrEqualTo(32.123456)
                .GreaterThanOrEqualTo(30.123456)
                .WithMessage("The Polygon Coordinate value is required and must be within the specified range.");

            //Validate Polygon Area 
            RuleFor(s => s.DeliveryAreaCoverage.Area)
                .GreaterThanOrEqualTo(50000);

            //Validate Polygon SRID
            RuleFor(s => s.DeliveryAreaCoverage.SRID)
                .Equal(4326);
        }
    }
}
