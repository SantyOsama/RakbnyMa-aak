using MediatR;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;
using RakbnyMa_aak.Services;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Drivers.RegisterDriver.Commands
{
    public class RegisterDriverCommandHandler : IRequestHandler<RegisterDriverCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudinaryService _cloudinary;
        private readonly IDriverVerificationService _verificationService;
        private readonly IDriverRepository _repo;

        public RegisterDriverCommandHandler(
            UserManager<ApplicationUser> userManager,
            ICloudinaryService cloudinary,
            IDriverVerificationService verificationService,
            IDriverRepository repo)
        {
            _userManager = userManager;
            _cloudinary = cloudinary;
            _verificationService = verificationService;
            _repo = repo;
        }
        
        public async Task<Response<string>> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DriverDto;

            // Upload images to Cloudinary
            var nationalIdImgUrl = await _cloudinary.UploadImageAsync(dto.NationalIdImage, "drivers/nationalId");
            var licenseImgUrl = await _cloudinary.UploadImageAsync(dto.DriverLicenseImage, "drivers/license");
            var carLicenseImgUrl = await _cloudinary.UploadImageAsync(dto.CarLicenseImage, "drivers/carlicense");
            var selfieImgUrl = await _cloudinary.UploadImageAsync(dto.SelfieImage, "drivers/selfie");

            // Face verification (commented out if not needed)
            /*
            bool isFaceValid = await _verificationService.MatchFaceAsync(selfieImgUrl, nationalIdImgUrl);
            if (!isFaceValid)
                return Response<string>.Fail("Face verification failed.");
            */

            // Create Identity User
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                UserName = dto.Email,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Picture = selfieImgUrl,
                UserType = UserType.Driver
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Response<string>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

            // Create Driver Profile
            var driver = new Driver
            {
                UserId = user.Id,
                NationalId = dto.NationalId,
                CarType = dto.CarType,
                CarCapacity = dto.CarCapacity,
                DriverNationalIdImagePath = nationalIdImgUrl,
                DriverLicenseImagePath = licenseImgUrl,
                CarLicenseImagePath = carLicenseImgUrl,
                SelfieImagePath = selfieImgUrl,
                IsFaceVerified = true,
                IsPhoneVerified = false,
                IsApproved = false
            };

            await _repo.AddAsync(driver);
            await _repo.SaveAsync();

            return Response<string>.Success("Driver registered successfully");
        }
    }

}
