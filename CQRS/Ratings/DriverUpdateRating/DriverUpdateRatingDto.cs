namespace RakbnyMa_aak.CQRS.Ratings.DriverUpdateRating
{
    public class DriverUpdateRatingDto
    {
        public int RatingId { get; set; }
        public string RaterId { get; set; }  
        public int? RatingValue { get; set; }
        public string? Comment { get; set; }
    }
}
