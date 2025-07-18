using RakbnyMa_aak.Models;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(
            string recipientUserId,
            ApplicationUser sender,
            string message,
            NotificationType type = NotificationType.إشعار_مخصص,
            string? relatedEntityId = null);
    }
}
