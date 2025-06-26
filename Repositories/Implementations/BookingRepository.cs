using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetBookingsByTripIdAsync(int tripId)
        {
            return await _context.Bookings
                .Where(b => b.TripId == tripId && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId && !b.IsDeleted)
                .Include(b => b.Trip)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingDetailsAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Trip)
                .ThenInclude(t => t.Driver)
                .FirstOrDefaultAsync(b => b.Id == bookingId && !b.IsDeleted);
        }

        public async Task<bool> IsUserAlreadyBookedAsync(string userId, int tripId)
        {
            return await _context.Bookings
                .AnyAsync(b => b.TripId == tripId && b.UserId == userId && !b.IsDeleted);
        }

        public async Task<int> GetTotalSeatsBookedAsync(int tripId)
        {
            return await _context.Bookings
                .Where(b => b.TripId == tripId && !b.IsDeleted)
                .SumAsync(b => b.NumberOfSeats);
        }
    }
}
