using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Repositories.Interfaces;
using RakbnyMa_aak.UOW;
using System.Security.Claims;

namespace RakbnyMa_aak.CQRS.Trips.UpdateTrip
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateTripCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<string>> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripDto.Id);
            if (trip == null)
                return Response<string>.Fail("Trip not found");

            var currentUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (trip.DriverId != currentUserId)
                return Response<string>.Fail("Unauthorized to update this trip");

            _mapper.Map(request.TripDto, trip);
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Trip updated successfully");
        }
    }
}
