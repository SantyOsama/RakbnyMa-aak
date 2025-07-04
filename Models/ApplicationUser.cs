using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please enter your full name.")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Full name must not exceed 100 characters.")]
        public string FullName { get; set; }

        [Display(Name = "Profile Picture")]
        public string Picture { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "User Type")]
        [Required(ErrorMessage = "Please select a user type.")]
        [EnumDataType(typeof(UserType))]
        [Column(TypeName = "nvarchar(20)")]
        public UserType UserType { get; set; }



        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select a gender.")]
        [EnumDataType(typeof(Gender))]
        [Column(TypeName = "nvarchar(20)")]
        public Gender Gender { get; set; }


        public virtual Driver? Driver { get; set; }
        public virtual ICollection<Rating>? RatingsGiven { get; set; }
        public virtual ICollection<Rating>? RatingsReceived { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }

        // User can send many messages
        public virtual ICollection<Message>? SentMessages { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }


        public ApplicationUser()
        {
            RatingsGiven = new HashSet<Rating>();
            RatingsReceived = new HashSet<Rating>();
            Bookings = new HashSet<Booking>();
            SentMessages = new HashSet<Message>();
            Notifications = new HashSet<Notification>();
        }

    }
}
