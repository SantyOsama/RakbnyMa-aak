using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.Data;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Hubs;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hub;

        public SendMessageCommandHandler(IUnitOfWork unitOfWork, IHubContext<ChatHub> hub)
        {
            _unitOfWork = unitOfWork;
            _hub = hub;
        }


        public async Task<Response<string>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.Dto.TripId);

            if (trip == null || trip.TripStatus != TripStatus.Ongoing)
                return Response<string>.Fail("The trip has not started or does not exist.");

            var approvedPassengerIds = await _unitOfWork.BookingRepository
                .FindAllAsync(b => b.TripId == request.Dto.TripId && b.RequestStatus == RequestStatus.Confirmed);

            var approvedPassengerIdsList = approvedPassengerIds.Select(b => b.UserId).ToList();

            var isSenderDriver = trip.DriverId == request.SenderId;
            var isSenderPassenger = approvedPassengerIdsList.Contains(request.SenderId);

            if (!isSenderDriver && !isSenderPassenger)
                return Response<string>.Fail("You are not part of this trip.");

            var message = new Message
            {
                TripId = request.Dto.TripId,
                SenderId = request.SenderId,
                Content = request.Dto.Content,
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();

            var allRecipients = approvedPassengerIdsList
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