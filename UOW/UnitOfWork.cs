using Microsoft.AspNetCore.Identity;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Implementations;
using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository UserRepository { get; }

        public IDriverRepository DriverRepository { get; }


        public UnitOfWork(AppDbContext dBContext, UserManager<ApplicationUser> userManager)
        {
            _context = dBContext;
            DriverRepository = new DriverRepository(_context);
          //  UserRepository= new UserRepository(userManager);

        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
