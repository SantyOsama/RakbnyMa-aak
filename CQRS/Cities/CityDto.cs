namespace RakbnyMa_aak.CQRS.Cities
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GovernorateId { get; set; }
        public string? GovernorateName { get; internal set; }
    }
}
