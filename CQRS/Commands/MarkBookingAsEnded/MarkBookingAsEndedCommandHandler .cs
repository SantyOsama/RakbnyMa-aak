using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.MarkBookingAsEnded
{
    public class MarkBookingAsEndedCommandHandler : IRequestHandler<MarkBookingAsEndedCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkBookingAsEndedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(MarkBookingAsEndedCommand request, CancellationToken cancellationToken)
        {
            request.Booking.HasEnded = true;
            _unitOfWork.BookingRepository.Update(request.Booking);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Booking marked as ended");
        }
    }
}

