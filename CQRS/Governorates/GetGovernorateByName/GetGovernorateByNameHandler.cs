using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Governorates.GetGovernorateByName
{
    public class GetGovernorateByNameHandler : IRequestHandler<GetGovernorateByNameQuery, Response<GovernorateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGovernorateByNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GovernorateDto>> Handle(GetGovernorateByNameQuery request, CancellationToken cancellationToken)
        {
            var governorate = await _unitOfWork.GovernorateRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync(g => g.Name.ToLower() == request.Name.ToLower() && !g.IsDeleted, cancellationToken);

            if (governorate == null)
                return Response<GovernorateDto>.Fail("Governorate not found.");

            var dto = _mapper.Map<GovernorateDto>(governorate);

            return Response<GovernorateDto>.Success(dto);
        }
    }
}
