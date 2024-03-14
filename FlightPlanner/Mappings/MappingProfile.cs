using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Airport, AirportViewModel>()
                .ForMember(viewModel => viewModel.Airport, 
                options => options.MapFrom(source => source.AirportCode));
            CreateMap<IEnumerable<Airport>, IEnumerable<AirportViewModel>>()
            .ConvertUsing(source =>
            source.Select(x => new AirportViewModel
            {
                Country = x.Country,
                City = x.City,             
                Airport = x.AirportCode,
            }));

            CreateMap<IEnumerable<AirportViewModel>, IEnumerable<Airport>>()
            .ConvertUsing(source =>
            source.Select(x => new Airport
            {
                Country = x.Country,
                City = x.City,
                AirportCode = x.Airport,
            }));

            CreateMap<AirportViewModel, Airport>()
                .ForMember(destination => destination.AirportCode,
                options => options.MapFrom(source => source.Airport));
            CreateMap<AddFlightRequest, Flight>();
            CreateMap<Flight, AddFlightResponse>();
        }
    }
}
