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

            if (string.IsNullOrEmpty(tripId) || string.IsNullOrEmpty(userId))
            {
                await base.OnConnectedAsync();
                return;
            }

            var trip = await _unitOfWork.TripRepository.GetByIdAsync(int.Parse(tripId));
            if (trip == null || trip.TripStatus != TripStatus.Ongoing)
            {
                await base.OnConnectedAsync();
                return;
            }

            var isDriver = trip.DriverId == userId;
            var isPassengerConfirmed = await _unitOfWork.BookingRepository.AnyAsync(
                b => b.TripId == trip.Id &&
                     b.UserId == userId &&
                     b.RequestStatus == RequestStatus.Confirmed);

            if (isDriver || isPassengerConfirmed)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tripId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var tripId = Context.GetHttpContext()?.Request.Query["tripId"];
            if (!string.IsNullOrEmpty(tripId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, tripId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
