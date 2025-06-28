using MediatR;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingExists;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
namespace RakbnyMa_aak.CQRS.Features.StartTripByPassenger
{
    public class StartTripByPassengerHandler : IRequestHandler<StartTripByPassengerCommand, Response<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public StartTripByPassengerHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(StartTripByPassengerCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ValidateBookingExistsCommand(request.BookingId));
            if (!result.IsSucceeded) return Response<bool>.Fail(result.Message);

            var booking = result.Data;

            if (booking.UserId != request.CurrentUserId)
                return Response<bool>.Fail("Unauthorized passenger.");

            if (booking.HasStarted)
                return Response<bool>.Fail("Trip already started by this passenger.");

            booking.HasStarted = true;
            _unitOfWork.BookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Passenger started trip.");
        }
    }
}
