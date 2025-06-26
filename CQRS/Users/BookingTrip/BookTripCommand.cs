using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Users.BookingTrip
{
    public class BookTripCommand : IRequest<Response<int>>
    {
        public BookTripDto TripDetails { get; }

        public BookTripCommand(BookTripDto tripDetails)
        {
            TripDetails = tripDetails;
        }
    }
}
