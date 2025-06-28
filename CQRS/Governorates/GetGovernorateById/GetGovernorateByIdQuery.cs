using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.GetGovernorateById
{
    public class GetGovernorateByIdQuery : IRequest<Response<GovernorateDto>>
    {
        public int Id { get; set; }

        public GetGovernorateByIdQuery(int id)
        {
            Id = id;
        }
    }
}
