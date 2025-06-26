using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<Response<AuthResponseDto>> LoginByUserTypeAsync(LoginDto dto, UserType expectedType)
        {
            var user = await _userManager.FindByEmailAsync(dto.EmailOrUsername)
                       ?? await _userManager.FindByNameAsync(dto.EmailOrUsername);

            if (user == null)
                return Response<AuthResponseDto>.Fail("User not found");

            if (user.UserType != expectedType)
                return Response<AuthResponseDto>.Fail("Unauthorized login for this role");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Response<AuthResponseDto>.Fail("Invalid credentials");

            var token = await _jwtService.GenerateToken(user); 

            return Response<AuthResponseDto>.Success(new AuthResponseDto
            {
                UserId = user.Id,
                Token = token,
                FullName = user.FullName,
                Role = user.UserType.ToString()
            }, "Login successful");
        }

    }

}
