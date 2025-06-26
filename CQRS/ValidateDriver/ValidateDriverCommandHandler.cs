using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.ValidateDriver
{
    public class ValidateDriverCommandHandler : IRequestHandler<ValidateDriverCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ValidateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.DriverRepository.GetByUserIdAsync(request.UserId);
            return driver != null
                ? Response<bool>.Success(true)
                : Response<bool>.Fail("You are not registered as a driver.");
        }
    }
}
