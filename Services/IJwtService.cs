using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);

    }
}
