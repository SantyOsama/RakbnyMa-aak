using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;
using RakbnyMa_aak.Services;

namespace RakbnyMa_aak.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;

        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper , ICloudinaryService cloudinaryService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _cloudinary = cloudinaryService;
        }

        public async Task<Response<string>> RegisterAsync(RegisterUserDto dto)
        {

            if (await _userManager.FindByEmailAsync(dto.Email) != null ||
                await _userManager.FindByNameAsync(dto.UserName) != null)
            {
                return Response<string>.Fail("Email or Username already exists");
            }

            string pictureUrl = null;
            if (dto.Picture != null)
            {
                pictureUrl = await _cloudinary.UploadImageAsync(dto.Picture, "users/profile");
            }

            var user = _mapper.Map<ApplicationUser>(dto);
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.UserType=Enums.Enums.UserType.User;
            user.Picture = pictureUrl;


            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Response<string>.Fail(errors);
            }


            return Response<string>.Success(user.Id, "User registered successfully");

        }
    }
}