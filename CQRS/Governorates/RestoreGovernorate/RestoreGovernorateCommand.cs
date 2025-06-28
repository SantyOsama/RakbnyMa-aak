using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.RestoreGovernorate
{
    public class RestoreGovernorateCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreGovernorateCommand(int id)
        {
            Id = id;
        }
    }
}
