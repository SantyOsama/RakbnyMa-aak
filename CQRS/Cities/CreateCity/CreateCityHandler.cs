using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.CreateCity
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<City>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;
            _unitOfWork.Cities.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return Response<string>.Success("City created");
        }
    }
}
