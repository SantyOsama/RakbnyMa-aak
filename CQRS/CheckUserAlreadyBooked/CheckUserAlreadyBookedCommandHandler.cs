using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.CheckUserAlreadyBooked
{
    public class CheckUserAlreadyBookedCommandHandler: IRequestHandler<CheckUserAlreadyBookedCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckUserAlreadyBookedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(CheckUserAlreadyBookedCommand request, CancellationToken cancellationToken)
        {
            var isBooked = await _unitOfWork.BookingRepository.IsUserAlreadyBookedAsync(request.UserId, request.TripId);
            return Response<bool>.Success(isBooked);
        }
    }
}
