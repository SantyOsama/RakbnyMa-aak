using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.UOW
{
    public interface IUnitOfWork
    {
        IDriverRepository DriverRepository {  get; }
        IUserRepository UserRepository { get; }
        IBookingRepository BookingRepository { get; }

        ITripRepository TripRepository { get; }
        Task<int> CompleteAsync(); // SaveChanges
        Task RollbackAsync(); // Rollback 

    }
}
