using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.ValidateDriver
{
    public class ValidateDriverCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
    }

   

}
