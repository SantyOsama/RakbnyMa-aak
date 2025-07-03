using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Services;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Services.Users
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;

        public UserRegistrationService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ICloudinaryService cloudinary)
        {
            _userManager = userManager;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<Response<string>> RegisterUserAsync(RegisterUserDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                return Response<string>.Fail("Email already exists");
            }

            string pictureUrl = null;
            if (dto.Picture != null)
            {
                pictureUrl = await _cloudinary.UploadImageAsync(dto.Picture, "users/profile");
            }

            var user = _mapper.Map<ApplicationUser>(dto);
          
            user.Email = dto.Email;
            user.UserName = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.UserType = UserType.User;
            user.Picture = pictureUrl;

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Response<string>.Fail(errors);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return Response<string>.Fail("User created but role assignment failed: " + errors);
            }

            return Response<string>.Success(user.Id, "User registered successfully");
        }
    }
}
