using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(ApplicationUser user);

    }
}
