using Microsoft.AspNetCore.SignalR;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.DTOs.Shared;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.Services.Interfaces;
using RakbnyMa_aak.SignalR;

public class NotificationService : INotificationService
{
    private readonly AppDbContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(AppDbContext context, IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string recipientUserId, ApplicationUser sender, string message)
    {
        // 1. Save to DB
        var notification = new Notification
        {
            UserId = recipientUserId,
            Message = message,
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };

        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();

        // 2. Send Real-time
        var payload = new NotificationDto
        {
            Message = message,
            SenderId = sender.Id,
            SenderFullName = sender.FullName,
            SenderPicture = sender.Picture
        };

        await _hubContext.Clients.User(recipientUserId)
            .SendAsync("Receive Notification", payload);
    }
}
