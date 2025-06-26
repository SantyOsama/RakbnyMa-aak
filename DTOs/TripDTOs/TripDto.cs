namespace RakbnyMa_aak.DTOs.TripDTOs
{
    public class TripDto
    {
        public string DriverId { get; set; }

        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int FromGovernorateId { get; set; }
        public int ToGovernorateId { get; set; }

        public string PickupLocation { get; set; }
        public string DestinationLocation { get; set; }

        public DateTime TripDate { get; set; }
        public TimeSpan Duration { get; set; }

        public int AvailableSeats { get; set; }
        public decimal PricePerSeat { get; set; }

        public bool IsRecurring { get; set; }
        public bool WomenOnly { get; set; }
    }
}
