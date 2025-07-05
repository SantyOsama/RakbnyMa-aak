using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Hubs;
using RakbnyMa_aak.Models;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Response<string>>
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _hub;

        public SendMessageCommandHandler(AppDbContext context, IHubContext<ChatHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<Response<string>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var trip = await _context.Trips.FindAsync(request.Dto.TripId);

            if (trip == null || trip.TripStatus != TripStatus.Ongoing)
                return Response<string>.Fail("The trip has not started or does not exist.");

            var approvedPassengerIds = await _context.Bookings
                .Where(b => b.TripId == request.Dto.TripId && b.RequestStatus == RequestStatus.Confirmed)
                .Select(b => b.UserId)
                .ToListAsync();

            var isSenderDriver = trip.DriverId == request.SenderId;
            var isSenderPassenger = approvedPassengerIds.Contains(request.SenderId);

            if (!isSenderDriver && !isSenderPassenger)
                return Response<string>.Fail("You are not part of this trip.");

            var message = new Message
            {
                TripId = request.Dto.TripId,
                SenderId = request.SenderId,
               // ReceiverId = null,
                Content = request.Dto.Content,
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            var allRecipients = approvedPassengerIds
                .Append(trip.DriverId)
                .Where(id => id != request.SenderId);

            foreach (var userId in allRecipients)
            {
                await _hub.Clients.User(userId).SendAsync("ReceiveGroupMessage", new
                {
                    message.TripId,
                    message.Content,
                    message.SenderId,
                    message.SentAt
                });
            }

            return Response<string>.Success("Message sent to all members of the trip.");
        }
    }
}
