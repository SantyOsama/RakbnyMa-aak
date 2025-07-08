using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.Models
{
    public class Payment : BaseEntity
    {
        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [Display(Name = "Booking")]
        public int? BookingId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [Display(Name = "Payment Method")]
        [EnumDataType(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "Payment status is required.")]
        [Display(Name = "Payment Status")]
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [Required(ErrorMessage = "Payment type is required.")]
        [Display(Name = "Payment Type")]
        [EnumDataType(typeof(PaymentType))]
        public PaymentType PaymentType { get; set; }

        [Display(Name = "Transaction ID")]
        [MaxLength(100, ErrorMessage = "Transaction ID must not exceed 100 characters.")]
        public string? TransactionId { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Failure Reason")]
        [MaxLength(500, ErrorMessage = "Failure reason must not exceed 500 characters.")]
        public string? FailureReason { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}