using RakbnyMa_aak.DTOs.DriverDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.Services
{
    public interface IDriverRegistrationService
    {
        Task<Response<string>> RegisterDriverAsync(RegisterDriverDto dto);

    }
}
