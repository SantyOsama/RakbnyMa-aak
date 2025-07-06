using MediatR;
using Microsoft.AspNetCore.SignalR;
using RakbnyMa_aak.CQRS.Chat.Commands;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Hubs;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Chat.Handlers
{
    public class SendChatMessageCommandHandler
        : IRequestHandler<SendChatMessageCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hub;

        public SendChatMessageCommandHandler(IUnitOfWork unitOfWork, IHubContext<ChatHub> hub)
        {
            _unitOfWork = unitOfWork;
            _hub = hub;
        }

        public async Task<Response<string>> Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.Dto.TripId);

            if (trip == null || trip.TripStatus != TripStatus.Ongoing)
                return Response<string>.Fail("الرحلة غير موجودة أو لم تبدأ بعد.");

            var isPassengerConfirmed = await _unitOfWork.BookingRepository
                .AnyAsync(b => b.TripId == trip.Id && b.UserId == request.SenderId && b.RequestStatus == RequestStatus.Confirmed);

            var isDriver = trip.DriverId == request.SenderId;

            if (!isPassengerConfirmed && !isDriver)
                return Response<string>.Fail("غير مسموح لك بالمشاركة في محادثة هذه الرحلة.");

            var message = new Message
            {
                TripId = request.Dto.TripId,
                Content = request.Dto.Content,
                SenderId = request.SenderId,
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();

            await _hub.Clients.Group(trip.Id.ToString()).SendAsync("ReceiveGroupMessage", new
            {
                TripId = message.TripId,
                Content = message.Content,
                SenderId = message.SenderId,
                SentAt = message.SentAt
            });

            return Response<string>.Success("تم إرسال الرسالة بنجاح.");
        }
    }
}
