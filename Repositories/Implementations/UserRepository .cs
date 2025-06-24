using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<string>> RegisterAsync(RegisterUserDto dto)
        {

            if (await _userManager.FindByEmailAsync(dto.Email) != null ||
                await _userManager.FindByNameAsync(dto.UserName) != null)
            {
                return Response<string>.Fail("Email or Username already exists");
            }

            var user = _mapper.Map<ApplicationUser>(dto);
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;

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