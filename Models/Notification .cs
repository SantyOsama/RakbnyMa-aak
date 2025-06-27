using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; }          
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
