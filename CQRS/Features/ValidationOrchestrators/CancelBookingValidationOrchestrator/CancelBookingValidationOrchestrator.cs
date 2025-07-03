using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.CancelBookingValidationOrchestrator
{
    public record CancelBookingValidationOrchestrator(int BookingId, string UserId)
      : IRequest<Response<CancelBookingValidationResultDto>>;

}
