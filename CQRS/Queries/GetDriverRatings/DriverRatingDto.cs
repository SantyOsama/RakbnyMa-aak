namespace RakbnyMa_aak.CQRS.Ratings.DriverGetRatings
{
    public class DriverRatingDto
    {
        public int RatingId { get; set; }
        public int TripId { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public string RaterName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
