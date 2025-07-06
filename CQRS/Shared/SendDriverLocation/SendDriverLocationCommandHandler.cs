using MediatR;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
namespace RakbnyMa_aak.CQRS.Commands.SendDriverLocation
{
    public class SendDriverLocationCommandHandler : IRequestHandler<SendDriverLocationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SendDriverLocationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SendDriverLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new TripTracking
            {
                TripId = request.TripId,
                CurrentLat = request.Latitude,
                CurrentLong = request.Longitude,
                Timestamp = DateTime.UtcNow
            };

            await _unitOfWork.TripTrackingRepository.AddAsync(location);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }

}
