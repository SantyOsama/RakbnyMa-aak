using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.RestoreTripSeats
{
    public class RestoreTripSeatsCommandHandler : IRequestHandler<RestoreTripSeatsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestoreTripSeatsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Response<bool>> Handle(RestoreTripSeatsCommand request, CancellationToken cancellationToken)
        {
            request.Trip.AvailableSeats += request.SeatsToRestore;
            request.Trip.UpdatedAt= DateTime.UtcNow;
            _unitOfWork.TripRepository.Update(request.Trip);
            return Task.FromResult(Response<bool>.Success(true));
        }
    }
}
