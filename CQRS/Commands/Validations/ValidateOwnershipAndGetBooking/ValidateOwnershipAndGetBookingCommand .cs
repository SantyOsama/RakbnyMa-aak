using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateOwnershipAndGetBooking
{
    public class ValidateOwnershipAndGetBookingCommand : IRequest<Response<Booking>>
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public ValidateOwnershipAndGetBookingCommand(int bookingId, string userId)
        {
            BookingId = bookingId;
            UserId = userId;
        }
    }

}
