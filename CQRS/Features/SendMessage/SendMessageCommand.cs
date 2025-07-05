using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.SendMessage
{
        public class SendMessageCommand : IRequest<Response<string>>
        {
            public string SenderId { get; set; }
            public SendMessageDto Dto { get; set; }

            public SendMessageCommand(string senderId, SendMessageDto dto)
            {
                SenderId = senderId;
                Dto = dto;
            }
        }

    
}
