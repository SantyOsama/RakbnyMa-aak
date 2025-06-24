using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<string>> RegisterAsync(RegisterUserDto dto);
    }
}