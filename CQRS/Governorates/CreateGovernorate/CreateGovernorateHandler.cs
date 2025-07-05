using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Governorates.CreateGovernorate
{
    public class CreateGovernorateHandler : IRequestHandler<CreateGovernorateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGovernorateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateGovernorateCommand request, CancellationToken cancellationToken)
        {
           
            var isExist = await _unitOfWork.GovernorateRepository
                .AnyAsync(g => g.Name == request.Dto.Name);

            if (isExist)
            {
                return Response<string>.Fail("This governorate already exists");
            }

            var entity = _mapper.Map<Governorate>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.GovernorateRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Governorate created");
        }

    }
}
