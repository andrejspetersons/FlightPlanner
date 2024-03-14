using FlightPlanner.Core.Models;
using FluentValidation;

namespace FlightPlanner.Validations
{
    public class SearchFlightValidator:AbstractValidator<SearchFlightsRequest>
    {
        public SearchFlightValidator()
        {
            RuleFor(searchRequest => searchRequest.From).NotEmpty();
            RuleFor(searchRequest => searchRequest.To).NotEmpty();
            RuleFor(searchRequest => searchRequest.DepartureDate).NotEmpty();
            RuleFor(searchRequest => searchRequest.From).NotEmpty().NotEqual(searchRequest => searchRequest.To);
        }
    }
}
