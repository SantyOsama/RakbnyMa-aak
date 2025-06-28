using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.Repositories.Implementations
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<string?> GetDriverIdByTripIdAsync(int tripId)
        {
            return await GetAllQueryable()
                .Where(t => t.Id == tripId)
                .Select(t => t.DriverId)
                .FirstOrDefaultAsync();
        }

    }
}
