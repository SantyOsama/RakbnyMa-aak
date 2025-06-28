using MediatR;
using RakbnyMa_aak.CQRS.Features.BookTripRequest;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.BookTripOrchestrator
{
    public class BookTripCommand : IRequest<Response<int>>
    {
        public BookTripDto TripDetails { get; set; }

        public BookTripCommand(BookTripDto tripDetails)
        {
            TripDetails = tripDetails;
        }

    }

}
