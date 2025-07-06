using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Queries.GetLastDriverLocation
{
    public class GetLastDriverLocationQueryHandler
        : IRequestHandler<GetLastDriverLocationQuery, Response<TripTracking>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLastDriverLocationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TripTracking>> Handle(GetLastDriverLocationQuery request, CancellationToken cancellationToken)
        {
            var tracking = await _unitOfWork.TripTrackingRepository.GetLastLocationAsync(request.TripId);

            if (tracking == null)
                return Response<TripTracking>.Fail("No location found for this trip.", null, 404);

            return Response<TripTracking>.Success(tracking, "Latest location retrieved successfully.");
        }
    }
}
