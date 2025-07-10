using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.CQRS.Features.Admin.ApproveDriver;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Services.Interfaces;
using RakbnyMa_aak.UOW;

public class ApproveDriverCommandHandler : IRequestHandler<ApproveDriverCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IEmailService _emailService;
    public ApproveDriverCommandHandler(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IBackgroundJobClient backgroundJobClient,
    IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _backgroundJobClient = backgroundJobClient;
        _emailService = emailService;
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

        if (!addRoleResult.Succeeded)
            return Response<bool>.Fail("Driver approved but failed to assign role: " +
                string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));

        _backgroundJobClient.Enqueue<IEmailService>(emailService =>
        emailService.SendEmailAsync(
        user.Email,
        "تم قبولك كسائق",
        @$"
            <div style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; max-width: 600px; margin: auto; border: 1px solid #ddd; border-radius: 10px; padding: 20px; background-color: #f9f9f9;"">
                <div style=""text-align: center;"">
                    <h2 style=""color: #2e7d32;"">🎉 تم قبولك كسائق!</h2>
                    <img src=""https://res.cloudinary.com/dbrz7pbsa/image/upload/v1752157282/logo_gicmcl.jpg"" alt=""Welcome Driver"" style=""max-width: 50%; height: auto; margin-bottom: 20px;"" />
                    
                </div>

                <p style=""font-size: 16px; color: #333;"">
                    مرحبًا <strong>{user.FullName}</strong>،
                </p>

                <p style=""font-size: 16px; color: #333;"">
                    يسرّنا إبلاغك بأنه تم قبولك كسائق في منصة <strong style=""color: #2e7d32;"">ركبني معاك</strong>.
                </p>

                <p style=""font-size: 16px; color: #333;"">
                    يمكنك الآن تسجيل الدخول إلى حسابك والاستفادة من كافة خدماتنا.
                </p>

                <p style=""font-size: 14px; color: gray; text-align: center;"">
                    مع تحيات<br/>
                    فريق <strong>ركبني معاك</strong> 💚
                </p>
               
            </div>"
       ));

        return Response<bool>.Success(true, "Driver approved and role assigned successfully.");
    }
}
