using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Admin.ApproveDriver
{
    public class ApproveDriverCommandHandler : IRequestHandler<ApproveDriverCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ApproveDriverCommand request, CancellationToken cancellationToken)
        {
            var driverRepo = _unitOfWork.DriverRepository;
            var driver = await driverRepo.GetByIdAsync(request.DriverId);

            if (driver == null)
                return Response<bool>.Fail("Driver not found", statusCode: 404);

            driver.IsApproved = true;
            driver.ApprovedAt = DateTime.UtcNow;

            driverRepo.Update(driver);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Driver approved successfully");
        }
    }
}
