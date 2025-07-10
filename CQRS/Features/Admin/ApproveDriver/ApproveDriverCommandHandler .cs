using MediatR;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.CQRS.Features.Admin.ApproveDriver;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

public class ApproveDriverCommandHandler : IRequestHandler<ApproveDriverCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApproveDriverCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Response<bool>> Handle(ApproveDriverCommand request, CancellationToken cancellationToken)
    {
        var driverRepo = _unitOfWork.DriverRepository;
        var driver = await driverRepo.GetByUserIdAsync(request.DriverId);

        if (driver == null)
            return Response<bool>.Fail("Driver not found", statusCode: 404);

        driver.IsApproved = true;
        driver.ApprovedAt = DateTime.UtcNow;

        driverRepo.Update(driver);
        await _unitOfWork.CompleteAsync();

        var user = await _userManager.FindByIdAsync(driver.UserId);

        if (user == null)
            return Response<bool>.Fail("User not found", statusCode: 404);

        var addRoleResult = await _userManager.AddToRoleAsync(user, "Driver");
        // check user has role Driver or not (if driver make update in his profile)
        bool isDriver = await _userManager.IsInRoleAsync(user, "Driver");

        if (!addRoleResult.Succeeded && !isDriver)
            return Response<bool>.Fail("Driver approved but failed to assign role: " +
                string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));

        return Response<bool>.Success(true, "Driver approved and role assigned successfully.");
    }
}
