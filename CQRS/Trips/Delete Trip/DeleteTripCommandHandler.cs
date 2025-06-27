using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using System.Security.Claims;

namespace RakbnyMa_aak.CQRS.Trips.Delete_Trip
{
    public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteTripCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<string>> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
            if (trip == null)
                return Response<string>.Fail("Trip not found");

            var currentUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trip.DriverId != currentUserId)
                return Response<string>.Fail("Unauthorized to delete this trip");

            _unitOfWork.TripRepository.Delete(trip);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Trip deleted successfully");
        }
    }
}
