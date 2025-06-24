using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.Repositories.Implementations
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext context) : base(context) { }

        public async Task<Driver?> GetByUserIdAsync(string userId)
        {
            return await _context.Drivers
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<IEnumerable<Driver>> GetPendingApprovalDriversAsync()
        {
            return await _context.Drivers
                .Where(d => !d.IsApproved)
                .ToListAsync();
        }
    }

}
