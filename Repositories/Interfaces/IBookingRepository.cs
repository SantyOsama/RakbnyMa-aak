using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<List<Booking>> GetBookingsByTripIdAsync(int tripId);
        Task<List<Booking>> GetBookingsByUserIdAsync(string userId);
        Task<Booking?> GetBookingDetailsAsync(int bookingId);
        Task<bool> IsUserAlreadyBookedAsync(string userId, int tripId);
        Task<int> GetTotalSeatsBookedAsync(int tripId);
    }
}
