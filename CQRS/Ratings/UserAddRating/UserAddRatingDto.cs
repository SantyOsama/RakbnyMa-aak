namespace RakbnyMa_aak.CQRS.Ratings.UserAddRating
{
    public class UserAddRatingDto
    {
        public int TripId { get; set; }
        public string RaterId { get; set; }
        public string ? Comment { get; set; }
        public int RatingValue { get; set; }
    }
}
