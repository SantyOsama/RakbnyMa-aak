using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.SendNotificationForDriver
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
