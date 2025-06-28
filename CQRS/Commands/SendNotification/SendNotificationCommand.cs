using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.SendNotification
{
    public class SendNotificationCommand : IRequest<Response<bool>>
    {
        public SendNotificationDto NotificationDto { get; set; }

        public SendNotificationCommand(SendNotificationDto dto)
        {
            NotificationDto = dto;
        }
    }
}
