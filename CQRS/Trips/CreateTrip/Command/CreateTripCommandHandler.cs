using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Trips.CreateTrip.Command
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTripCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            var dto = request.TripDto;

          
            var driver = await _unitOfWork.DriverRepository.GetByUserIdAsync(dto.DriverId);
            if (driver == null)
                return Response<int>.Fail("You are not registered as a driver.");

           
            if (dto.FromCityId == dto.ToCityId)
                return Response<int>.Fail("Departure city cannot be the same as destination city.");

            if (dto.AvailableSeats <= 1)
                return Response<int>.Fail("Available seats must be greater than 1.");


            var trip = new Trip
            {
                DriverId = dto.DriverId,
                FromCityId = dto.FromCityId,
                ToCityId = dto.ToCityId,
                FromGovernorateId = dto.FromGovernorateId,
                ToGovernorateId = dto.ToGovernorateId,
                PickupLocation = dto.PickupLocation,
                DestinationLocation = dto.DestinationLocation,
                TripDate = dto.TripDate,
                TripStatus = TripStatus.Scheduled,
                //Duration = TimeSpan,
                AvailableSeats = dto.AvailableSeats,
                PricePerSeat = dto.PricePerSeat,
                IsRecurring = dto.IsRecurring,
                WomenOnly = dto.WomenOnly,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };


            await _unitOfWork.TripRepository.AddAsync(trip);
            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(trip.Id,"Trip has been created successfully.");
        }
    }
}
