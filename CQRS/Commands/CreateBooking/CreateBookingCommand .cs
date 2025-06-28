using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<Response<int>>
    {
        public CreateBookingDto BookingDto { get; set; }

        public CreateBookingCommand(CreateBookingDto bookingDto)
        {
            BookingDto = bookingDto;
        }
    }

}
