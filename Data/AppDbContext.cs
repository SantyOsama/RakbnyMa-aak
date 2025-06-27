using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripTracking> TripTrackings { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🟢 Global Query Filters (Soft Delete)
            builder.Entity<Trip>().HasQueryFilter(t => !t.IsDeleted);
            builder.Entity<Booking>().HasQueryFilter(t => !t.IsDeleted);
            builder.Entity<Rating>().HasQueryFilter(t => !t.IsDeleted);
            builder.Entity<Message>().HasQueryFilter(t => !t.IsDeleted);

            // 🟢 UNIQUE CONSTRAINTS

            // 1. Ensure each user can only rate another user once per trip
            builder.Entity<Rating>()
                .HasIndex(r => new { r.RaterId, r.RatedId, r.TripId })
                .IsUnique();

            // 2. NationalId for drivers must be unique
            builder.Entity<Driver>()
                .HasIndex(d => d.NationalId)
                .IsUnique();

            // 3. City name must be unique inside a Governorate
            builder.Entity<City>()
                .HasIndex(c => new { c.Name, c.GovernorateId })
                .IsUnique();

            // 4. Governorate name must be unique
            builder.Entity<Governorate>()
                .HasIndex(g => g.Name)
                .IsUnique();

            // 🟢 CHECK CONSTRAINTS

            // 1. RatingValue between 1 and 5
            builder.Entity<Rating>()
                .HasCheckConstraint("CK_Rating_RatingValue", "[RatingValue] BETWEEN 1 AND 5");

            // 2. NumberOfSeats > 0 in Booking
            builder.Entity<Booking>()
                .HasCheckConstraint("CK_Booking_NumberOfSeats", "[NumberOfSeats] > 0");

            // 3. PricePerSeat > 0 in Trip
            builder.Entity<Trip>()
                .HasCheckConstraint("CK_Trip_PricePerSeat", "[PricePerSeat] > 0");

            // 4. AvailableSeats >= 0 in Trip
            builder.Entity<Trip>()
                .HasCheckConstraint("CK_Trip_AvailableSeats", "[AvailableSeats] >= 0");

            // 🔧 حل مشكلة RatingsGiven و RatingsReceived
            builder.Entity<Rating>()
                .HasOne(r => r.Rater)
                .WithMany(u => u.RatingsGiven)
                .HasForeignKey(r => r.RaterId)
                .OnDelete(DeleteBehavior.Restrict); // اختياري حسب المطلوب

            builder.Entity<Rating>()
                .HasOne(r => r.Rated)
                .WithMany(u => u.RatingsReceived)
                .HasForeignKey(r => r.RatedId)
                .OnDelete(DeleteBehavior.Restrict); // علشان ما يعملش Cascade Delete على اليوزر
        }

    }
}
