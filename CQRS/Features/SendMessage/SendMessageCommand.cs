using MediatR;
using RakbnyMa_aak.CQRS.Features.SendMessage;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Chat.Commands
{
    public record SendChatMessageCommand(string SenderId, SendMessageDto Dto)
        : IRequest<Response<string>>;
}