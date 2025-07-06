using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Trip.Commands.UpdateTrip
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTripCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            var existingTrip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);

            if (existingTrip == null || existingTrip.IsDeleted)
                return Response<int>.Fail("Trip not found.");

            _mapper.Map(request.TripDto, existingTrip);

            existingTrip.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.TripRepository.Update(existingTrip);
            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(existingTrip.Id, "Trip updated successfully.");
        }
    }

}
