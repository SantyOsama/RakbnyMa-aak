using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.CreateBooking
{
    public record CreateBookingCommand(CreateBookingDto BookingDto) : IRequest<Response<int>>;

}
