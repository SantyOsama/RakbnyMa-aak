using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.MarkBookingAsEnded
{
    public record MarkBookingAsEndedCommand(Booking Booking) : IRequest<Response<bool>>;

}
