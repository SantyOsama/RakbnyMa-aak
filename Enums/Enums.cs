namespace RakbnyMa_aak.Enums
{
    public class Enums
    {
        public enum UserType
        {
            Driver,
            User,
        }

        public enum CarType
        {
            Sedan,
            SUV,
            Van,
            Bus
        }

        public enum TripStatus
        {
            Scheduled,
            Ongoing,
            Completed,
            Cancelled
        }

        public enum RequestStatus
        {
            Pending,
            Confirmed,
            Cancelled,
            Rejected
        }
    }
}
