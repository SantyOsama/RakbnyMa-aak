namespace RakbnyMa_aak.CQRS.Ratings.UserUpdateRating
{
    public class UserUpdateRatingDto
    {
        public int RatingId { get; set; }
        public string RaterId { get; set; }
        public int? RatingValue { get; set; }
        public string? Comment { get; set; }
    }
}
