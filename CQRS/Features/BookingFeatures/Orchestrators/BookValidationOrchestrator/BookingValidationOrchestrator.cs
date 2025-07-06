using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Features.Booking.Orchestrators.BookValidationOrchestrator
{
    public record BookingValidationOrchestrator(int BookingId, string CurrentUserId)
        : IRequest<Response<BookingValidationResultDto>>;
}
