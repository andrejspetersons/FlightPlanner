using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Airports
{
    public class SearchKeywordCommand:IRequest<ServiceResult>
    {
        public string Keyword { get; set; }

        public SearchKeywordCommand(string keyword)
        {
            Keyword = keyword;    
        }
    }
}
