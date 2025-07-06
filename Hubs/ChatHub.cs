using Microsoft.AspNetCore.SignalR;
using RakbnyMa_aak.UOW;
using System.Security.Claims;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var tripId = httpContext?.Request.Query["tripId"].ToString();
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // If tripId or userId is missing, skip group joining
            if (string.IsNullOrEmpty(tripId) || string.IsNullOrEmpty(userId))
            {
                await base.OnConnectedAsync();
                return;
            }

            var trip = await _unitOfWork.TripRepository.GetByIdAsync(int.Parse(tripId));

            // If trip doesn't exist or is not ongoing, skip group joining
            if (trip == null || trip.TripStatus != TripStatus.Ongoing)
            {
                await base.OnConnectedAsync();
                return;
            }

            // Check if the user is the driver
            bool isDriver = trip.DriverId == userId;

            // Check if the user is a confirmed passenger
            var isPassengerConfirmed = await _unitOfWork.BookingRepository
                .AnyAsync(b => b.TripId == trip.Id &&
                               b.UserId == userId &&
                               b.RequestStatus == RequestStatus.Confirmed);

            // If the user is either the driver or a confirmed passenger, add them to the group
            if (isDriver || isPassengerConfirmed)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tripId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var tripId = Context.GetHttpContext()?.Request.Query["tripId"];

            // Remove user from group on disconnection
            if (!string.IsNullOrEmpty(tripId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, tripId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
