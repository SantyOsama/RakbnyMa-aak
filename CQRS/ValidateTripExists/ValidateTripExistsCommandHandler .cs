using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.ValidateTripExists
{
    public class ValidateTripExistsCommandHandler : IRequestHandler<ValidateTripExistsCommand, Response<Trip>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateTripExistsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Trip>> Handle(ValidateTripExistsCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
            if (trip == null || trip.IsDeleted)
                return Response<Trip>.Fail("Trip not found.");

            return Response<Trip>.Success(trip);
        }
    }

}
