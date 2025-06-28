using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;
using System.Linq.Expressions;

namespace RakbnyMa_aak.Repositories.Implementations
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<City>> GetAllAsync(Expression<Func<City, bool>> filter)
        {
            return await _context.Set<City>().Where(filter).ToListAsync();
        }


    }
}
