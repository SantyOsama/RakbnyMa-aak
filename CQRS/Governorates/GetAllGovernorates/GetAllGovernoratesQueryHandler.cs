using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Governorates.GetAllGovernorates
{
    public class GetAllGovernoratesHandler : IRequestHandler<GetAllGovernoratesQuery, Response<List<GovernorateDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllGovernoratesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<GovernorateDto>>> Handle(GetAllGovernoratesQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Governorates.GetAllAsync();
            return Response<List<GovernorateDto>>.Success(_mapper.Map<List<GovernorateDto>>(list));
        }
    }
}
