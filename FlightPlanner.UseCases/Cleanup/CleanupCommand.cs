using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Cleanup
{
    public class CleanupCommand:IRequest<ServiceResult>
    {
        public CleanupCommand()
        {
                
        }
    }
}
