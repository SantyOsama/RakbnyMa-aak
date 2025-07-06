using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Governorates.DeleteGovernorate
{
    public class DeleteGovernorateHandler : IRequestHandler<DeleteGovernorateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGovernorateHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(DeleteGovernorateCommand request, CancellationToken cancellationToken)
        {
            var governorate = await _unitOfWork.GovernorateRepository.GetByIdAsync(request.Id);

            if (governorate == null || governorate.IsDeleted)
                return Response<string>.Fail("Governorate not found.");

            // Soft delete for Governorate
            governorate.IsDeleted = true;
            governorate.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.GovernorateRepository.Update(governorate);

            // Get related cities
            var relatedCities = await _unitOfWork.CityRepository.GetAllAsync(c => c.GovernorateId == request.Id && !c.IsDeleted);

            foreach (var city in relatedCities)
            {
                city.IsDeleted = true;
                city.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.CityRepository.Update(city);
            }

            await _unitOfWork.CompleteAsync();
            return Response<string>.Success("Governorate and its cities soft-deleted successfully.");
        }
    }
}
