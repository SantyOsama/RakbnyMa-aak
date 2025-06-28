using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Governorates.DeleteGovernorate
{
    public class DeleteGovernorateHandler : IRequestHandler<DeleteGovernorateCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGovernorateHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(DeleteGovernorateCommand request, CancellationToken cancellationToken)
        {
            var governorate = await _unitOfWork.Governorates.GetByIdAsync(request.Id);

            if (governorate == null || governorate.IsDeleted)
                return Response<string>.Fail("Governorate not found.");

            try
            {
                // Soft delete for Governorate
                governorate.IsDeleted = true;
                governorate.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Governorates.Update(governorate);

                // Get related cities
                var relatedCities = await _unitOfWork.Cities.GetAllAsync(c => c.GovernorateId == request.Id && !c.IsDeleted);

                foreach (var city in relatedCities)
                {
                    city.IsDeleted = true;
                    city.UpdatedAt = DateTime.UtcNow;
                    _unitOfWork.Cities.Update(city);
                }

                await _unitOfWork.CompleteAsync();
                return Response<string>.Success("Governorate and its cities soft-deleted successfully.");
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return Response<string>.Fail($"Soft delete failed: {error}");
            }
        }
    }
}