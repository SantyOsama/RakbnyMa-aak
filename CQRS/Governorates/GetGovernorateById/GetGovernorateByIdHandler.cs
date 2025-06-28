using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Governorates.GetGovernorateById
{
    public class GetGovernorateByIdHandler : IRequestHandler<GetGovernorateByIdQuery, Response<GovernorateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGovernorateByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GovernorateDto>> Handle(GetGovernorateByIdQuery request, CancellationToken cancellationToken)
        {
            var governorate = await _unitOfWork.Governorates
                .GetAllQueryable()
                .FirstOrDefaultAsync(g => g.Id == request.Id && !g.IsDeleted, cancellationToken);

            if (governorate == null)
                return Response<GovernorateDto>.Fail("Governorate not found.");

            var dto = _mapper.Map<GovernorateDto>(governorate);

            return Response<GovernorateDto>.Success(dto);
        }
    }
}
