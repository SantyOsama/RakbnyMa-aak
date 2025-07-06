using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string recipientUserId, ApplicationUser sender, string message);
    }
}
