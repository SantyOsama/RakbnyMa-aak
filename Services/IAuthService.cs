using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.Services
{
    public interface IAuthService
    {
        Task<Response<AuthResponseDto>> LoginByUserTypeAsync(LoginDto dto, Enums.Enums.UserType user);

    }
}
