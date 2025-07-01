namespace RakbnyMa_aak.CQRS.Queries.GetUserRatings
{
    public class UserRatingDto
    {
        public int RatingId { get; set; }
        public int TripId { get; set; }
        public string DriverName { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
