namespace RakbnyMa_aak.CQRS.Queries.GetUserRatings
{
    public class GetUserRatingDto
    {
        public string RaterId { get; set; }
        public int? StarsFilter { get; set; } 
        public bool? HasComment { get; set; } 

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
