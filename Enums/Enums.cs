using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Enums
{
    public class Enums
    {
        public enum UserType
        {
            Driver,
            User,
            Admin
        }

        public enum Gender
        {
            [Display(Name = "Male")]
            Male,

            [Display(Name = "Female")]
            Female
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

        public enum CarColor
        {
            [Display(Name = "Black")]
            Black,

            [Display(Name = "White")]
            White,

            [Display(Name = "Red")]
            Red,

            [Display(Name = "Blue")]
            Blue,

            [Display(Name = "Green")]
            Green,

            [Display(Name = "Yellow")]
            Yellow,

            [Display(Name = "Silver")]
            Silver,

            [Display(Name = "Gray")]
            Gray,

            [Display(Name = "Brown")]
            Brown,

            [Display(Name = "Orange")]
            Orange,

            [Display(Name = "Gold")]
            Gold,

            [Display(Name = "Purple")]
            Purple,

            [Display(Name = "Pink")]
            Pink,

            [Display(Name = "Beige")]
            Beige,

            [Display(Name = "Maroon")]
            Maroon,

            [Display(Name = "Navy")]
            Navy
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
