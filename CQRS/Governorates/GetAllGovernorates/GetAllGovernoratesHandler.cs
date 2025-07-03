using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using RakbnyMa_aak.CQRS.Governorates;
using Microsoft.EntityFrameworkCore;


namespace RakbnyMa_aak.CQRS.Governorates.GetAllGovernorates
{
    public class GetAllGovernoratesHandler : IRequestHandler<GetAllGovernoratesQuery, Response<PaginatedResult<GovernorateDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllGovernoratesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedResult<GovernorateDto>>> Handle(GetAllGovernoratesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.GovernorateRepository.GetAllQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedItems = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<GovernorateDto>(
                items: _mapper.Map<List<GovernorateDto>>(pagedItems),
                totalCount: totalCount,
                page: request.Page,
                pageSize: request.PageSize
            );

            return Response<PaginatedResult<GovernorateDto>>.Success(result);
        }

    }

}
