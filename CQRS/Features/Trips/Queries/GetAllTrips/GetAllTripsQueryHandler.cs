using AutoMapper;
using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Trips.Queries.GetAllTrips
{
    public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQuery, PaginatedResult<TripResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTripsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<TripResponseDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TripRepository
                .GetProjectedPaginatedAsync<TripResponseDto>(
                    predicate: _ => true,
                    page: request.Page,
                    pageSize: request.PageSize,
                    mapper: _mapper,
                    cancellationToken: cancellationToken
                );
        }
    }
}
