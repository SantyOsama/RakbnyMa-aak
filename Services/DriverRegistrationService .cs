using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.DTOs.DriverDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Services.Drivers
{
    public class DriverRegistrationService : IDriverRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudinaryService _cloudinary;
        private readonly IDriverVerificationService _verificationService;
        private readonly IDriverRepository _repo;

        public DriverRegistrationService(
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

        public async Task<Response<string>> RegisterDriverAsync(RegisterDriverDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null ||
                await _userManager.FindByNameAsync(dto.FullName) != null)
            {
                return Response<string>.Fail("Email or Username already exists.");
            }
            //var uploadTasks = new[]
            //    {
            //        _cloudinary.UploadImageAsync(dto.NationalIdImage, "drivers/nationalId"),
            //        _cloudinary.UploadImageAsync(dto.DriverLicenseImage, "drivers/license"),
            //        _cloudinary.UploadImageAsync(dto.CarLicenseImage, "drivers/carlicense"),
            //        _cloudinary.UploadImageAsync(dto.SelfieImage, "drivers/selfie")
            //    };

            //var uploadResults = await Task.WhenAll(uploadTasks);
            //if (uploadResults.Any(url => string.IsNullOrEmpty(url)))
            //    return Response<string>.Fail("Failed to upload one or more images.");

            //Upload images
            var nationalIdImgUrl = await _cloudinary.UploadImageAsync(dto.NationalIdImage, "drivers/nationalId");
            var licenseImgUrl = await _cloudinary.UploadImageAsync(dto.DriverLicenseImage, "drivers/license");
            var carLicenseImgUrl = await _cloudinary.UploadImageAsync(dto.CarLicenseImage, "drivers/carlicense");
            var selfieImgUrl = await _cloudinary.UploadImageAsync(dto.SelfieImage, "drivers/selfie");

            // Optional: Face verification
            /*
            bool isFaceValid = await _verificationService.MatchFaceAsync(selfieImgUrl, nationalIdImgUrl);
            if (!isFaceValid)
                return Response<string>.Fail("Face verification failed.");
            */

            //var nationalIdImgUrl = uploadResults[0];
            //var licenseImgUrl = uploadResults[1];
            //var carLicenseImgUrl = uploadResults[2];
            //var selfieImgUrl = uploadResults[3];
            // Create Identity User
            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Picture = selfieImgUrl,
                UserType = UserType.Driver
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Response<string>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, "Driver");
            if (!roleResult.Succeeded)
                return Response<string>.Fail("User created, but failed to assign role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            // Create Driver entity
            var driver = new Driver
            {
                UserId = user.Id,
                NationalId = dto.NationalId,
                CarType = dto.CarType,
                CarColor=dto.CarColor,
                CarCapacity = dto.CarCapacity,
                CarModel=dto.CarModel,
                DriverNationalIdImagePath = nationalIdImgUrl,
                DriverLicenseImagePath = licenseImgUrl,
                CarLicenseImagePath = carLicenseImgUrl,
                SelfieImagePath = selfieImgUrl,
                IsFaceVerified = false, // or isFaceValid if used
                IsPhoneVerified = false,
                IsApproved = false
            };

            await _repo.AddAsync(driver);
            await _repo.CompleteAsync();

            return Response<string>.Success("Driver registered successfully");
        }
    }
}
