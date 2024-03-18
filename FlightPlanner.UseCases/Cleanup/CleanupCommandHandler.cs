using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Cleanup
{
    public class CleanupCommandHandler : IRequestHandler<CleanupCommand, ServiceResult>
    {
        private readonly IDbService _dbService;

        public CleanupCommandHandler(IDbService dbService)
        {
            _dbService = dbService;
        }
        public Task<ServiceResult> Handle(CleanupCommand request, CancellationToken cancellationToken)
        {
            _dbService.DeleteAll<Flight>();
            _dbService.DeleteAll<Airport>();

            return Task.FromResult(new ServiceResult());
        }
    }
}
