using System.ComponentModel.DataAnnotations;

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
            [Display(Name = "Sedan")]
            Sedan,

            [Display(Name = "SUV")]
            SUV,

            [Display(Name = "Van")]
            Van,

            [Display(Name = "Bus")]
            Bus,

            [Display(Name = "Taxi")]
            Taxi,

            [Display(Name = "Motorcycle")]
            Motorcycle,

            [Display(Name = "Pickup Truck")]
            PickupTruck,

            [Display(Name = "Minibus")]
            Minibus
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
