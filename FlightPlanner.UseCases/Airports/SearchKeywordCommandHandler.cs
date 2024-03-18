using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Airports
{
    public class SearchKeywordCommandHandler : IRequestHandler<SearchKeywordCommand, ServiceResult>
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public SearchKeywordCommandHandler(IAirportService airportService, IMapper mapper)
        {
            _airportService = airportService;
            _mapper = mapper;
        }
        public Task<ServiceResult> Handle(SearchKeywordCommand request, CancellationToken cancellationToken)
        {
            var airports = _airportService.AirportsByKeywords(request.Keyword);
            return Task.FromResult(new ServiceResult
            {
                ResultObject = _mapper.Map<IEnumerable<AirportViewModel>>(airports),
                Status = HttpStatusCode.OK
            });
        }
    }
}
