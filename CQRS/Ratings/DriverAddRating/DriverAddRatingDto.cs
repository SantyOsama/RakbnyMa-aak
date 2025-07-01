namespace RakbnyMa_aak.CQRS.Ratings.DriverAddRating
{
    public class DriverAddRatingDto
    {
        public int TripId { get; set; }
        public string RaterId { get; set; } 
        public string RatedPassengerId { get; set; }
        public string? Comment { get; set; }
        public int RatingValue { get; set; }
    }
}
