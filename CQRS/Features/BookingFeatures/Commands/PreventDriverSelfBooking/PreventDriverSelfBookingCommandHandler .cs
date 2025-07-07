using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.BookingFeatures.Commands.PreventDriverSelfBooking
{
    public class PreventDriverSelfBookingCommandHandler : IRequestHandler<PreventDriverSelfBookingCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PreventDriverSelfBookingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(PreventDriverSelfBookingCommand request, CancellationToken cancellationToken)
        {
            if (request.DriverId == request.UserId)
                return Response<string>.Fail("Drivers cannot book their own trips.");

            return Response<string>.Success("User is not the driver. Booking allowed.");
        }
    }
}
