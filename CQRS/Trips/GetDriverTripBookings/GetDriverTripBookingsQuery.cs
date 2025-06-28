using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Trips.GetDriverTripBookings
{
    public class GetDriverTripBookingsQuery : IRequest<Response<List<BookingForDriverDto>>>
    {
        public string DriverUserId { get; set; }

        public GetDriverTripBookingsQuery(string driverUserId)
        {
            DriverUserId = driverUserId;
        }
    }
}
