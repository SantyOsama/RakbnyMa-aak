using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingExists
{
    public class ValidateBookingExistsCommandHandler : IRequestHandler<ValidateBookingExistsCommand, Response<Booking>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateBookingExistsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Booking>> Handle(ValidateBookingExistsCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository
                 .GetByIdQueryable(request.BookingId)
                 .Select(b => new Booking
                 {
                     Id = b.Id,
                     RequestStatus = b.RequestStatus,
                     IsDeleted = b.IsDeleted,
                 })
                 .FirstOrDefaultAsync();
            if (booking == null)
                return Response<Booking>.Fail("Booking not found.");
            if (booking.IsDeleted || booking.RequestStatus == RequestStatus.Cancelled)
                return Response<Booking>.Fail("Booking is invalid (deleted or canceled).");

            return Response<Booking>.Success(booking);
        }
    }

}
