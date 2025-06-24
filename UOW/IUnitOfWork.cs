using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.UOW
{
    public interface IUnitOfWork
    {
        IDriverRepository DriverRepository {  get; }
        IUserRepository UserRepository { get; }
        Task<int> CompleteAsync(); // SaveChanges
        Task RollbackAsync(); // Rollback 

    }
}
