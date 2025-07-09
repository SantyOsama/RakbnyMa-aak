using MediatR;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Drivers.UpdateDriverProfile
{
    public class UpdateDriverProfileCommandHandler : IRequestHandler<UpdateDriverProfileCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateDriverProfileCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(UpdateDriverProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.DriverUserId);
            if (user == null)
                return Response<string>.Fail("User not found");

            user.FullName = request.Dto.FullName;
            user.Email = request.Dto.Email;
            user.PhoneNumber = request.Dto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded
                ? Response<string>.Success("Profile updated successfully")
                : Response<string>.Fail("Error: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

}
