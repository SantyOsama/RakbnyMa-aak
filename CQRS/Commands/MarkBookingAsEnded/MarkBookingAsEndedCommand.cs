using MediatR;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingForEnding;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.MarkBookingAsEnded
{
    public record MarkBookingAsEndedCommand(int BookingId) : IRequest<Response<bool>>;

}
