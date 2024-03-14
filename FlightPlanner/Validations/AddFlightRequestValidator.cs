using FlightPlanner.Models;
using FluentValidation;

namespace FlightPlanner.Validations
{
    public class AddFlightRequestValidator:AbstractValidator<AddFlightRequest>
    {
        public AddFlightRequestValidator()
        {
            RuleFor(request => request.Carrier).NotEmpty();
            RuleFor(request => request.ArrivalTime).NotEmpty();
            RuleFor(request => request.DepartureTime).NotEmpty();
            RuleFor(request => request.To).SetValidator(new AirportViewModelValidator());
            RuleFor(request => request.From).SetValidator(new AirportViewModelValidator());
            RuleFor(request => DateTime.Parse(request.ArrivalTime)).GreaterThan(request=>DateTime.Parse(request.DepartureTime));
        }
    }
}
