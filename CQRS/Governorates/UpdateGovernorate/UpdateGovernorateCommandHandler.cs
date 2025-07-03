using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Governorates.UpdateGovernorate
{
    public class UpdateGovernorateHandler : IRequestHandler<UpdateGovernorateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGovernorateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateGovernorateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GovernorateRepository.GetByIdAsync(request.Dto.Id);
            if (entity == null)
                return Response<string>.Fail("Not found");

            _mapper.Map(request.Dto, entity);
            _unitOfWork.GovernorateRepository.Update(entity);
            await _unitOfWork.CompleteAsync();
            return Response<string>.Success("Updated");
        }
    }
}
