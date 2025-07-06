using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.DTOs.DriverDTOs
{
    public class PendingDriverDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string CarModel { get; set; }
        public CarType CarType { get; set; }
        public bool IsApproved { get; set; }
    }
}
