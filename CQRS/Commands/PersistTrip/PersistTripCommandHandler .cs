using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.PersistTrip
{
    public class PersistTripCommandHandler : IRequestHandler<PersistTripCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersistTripCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(PersistTripCommand request, CancellationToken cancellationToken)
        {
            var trip = _mapper.Map<Trip>(request.TripDto);
            trip.TripStatus = TripStatus.Scheduled;
            await _unitOfWork.TripRepository.AddAsync(trip);
            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(trip.Id, $"Trip from {trip.FromCityId} to {trip.ToCityId} created successfully.");
        }
    }
}
