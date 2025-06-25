using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.Services
{
    public interface IUserRegistrationService
    {
        Task<Response<string>> RegisterUserAsync(RegisterUserDto dto);

    }
}
