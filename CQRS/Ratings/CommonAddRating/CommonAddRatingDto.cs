namespace RakbnyMa_aak.CQRS.Ratings.CommonAddRating
{
    public class CommonAddRatingDto
    {
        public int TripId { get; set; }
        public string RaterId { get; set; }          
        public string RatedId { get; set; }         
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public bool IsDriverRating { get; set; }     // True: driver rating passenger | False: passenger rating driver
    }

}
