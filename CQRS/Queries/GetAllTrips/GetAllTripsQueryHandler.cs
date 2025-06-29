using AutoMapper;
using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Queries.GetAllTrips
{
    public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQuery, Response<List<TripDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTripsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<TripDto>>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            var allTrips = await _unitOfWork.TripRepository.GetAllAsync();
            var trips = allTrips.Where(t => !t.IsDeleted).ToList();
            var result = _mapper.Map<List<TripDto>>(trips);

            return Response<List<TripDto>>.Success(result);
        }
    }
}
