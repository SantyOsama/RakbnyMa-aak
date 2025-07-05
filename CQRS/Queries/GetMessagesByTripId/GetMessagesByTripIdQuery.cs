using MediatR;
using RakbnyMa_aak.CQRS.Features.SendMessage;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Queries.GetMessagesByTripId
{
    public record GetMessagesByTripIdQuery(int TripId) : IRequest<Response<List<MessageDto>>>;

}
