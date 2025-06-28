using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateUserOwnsBooking
{
    public class ValidateUserOwnsBookingCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public ValidateUserOwnsBookingCommand(int bookingId, string userId)
        {
            BookingId = bookingId;
            UserId = userId;
        }
    }

}
