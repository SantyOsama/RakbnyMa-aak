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
            var dto = request.checkUserAlreadyBookedDto;

            if (string.IsNullOrWhiteSpace(dto.UserId) || dto.TripId <= 0)
                return Response<bool>.Fail("Invalid request data.");

            var isBooked = await _unitOfWork.BookingRepository.IsUserAlreadyBookedAsync(dto.UserId, dto.TripId);

            return Response<bool>.Success(isBooked);
        }

    }
}
