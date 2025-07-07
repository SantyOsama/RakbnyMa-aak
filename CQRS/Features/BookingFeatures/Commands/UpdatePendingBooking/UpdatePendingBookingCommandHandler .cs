using MediatR;
using RakbnyMa_aak.DTOs.BookingsDTOs.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.BookingFeatures.Commands.UpdatePendingBooking
{
    public class UpdatePendingBookingCommandHandler : IRequestHandler<UpdatePendingBookingCommand, Response<UpdateBookingSeatsResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePendingBookingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UpdateBookingSeatsResponseDto>> Handle(UpdatePendingBookingCommand request, CancellationToken cancellationToken)
        {
            var dto = request.BookingDto;
            var difference = dto.NewNumberOfSeats - request.OldSeats;

            if (difference < 0)
                return Response<UpdateBookingSeatsResponseDto>.Fail("Cannot decrease seats before approval");

            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(dto.BookingId);
            if (booking is null || booking.UserId != dto.UserId)
                return Response<UpdateBookingSeatsResponseDto>.Fail("Booking not found or access denied");

            booking.NumberOfSeats = dto.NewNumberOfSeats;
            booking.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();

            return Response<UpdateBookingSeatsResponseDto>.Success(new UpdateBookingSeatsResponseDto
            {
                BookingId = dto.BookingId,
                OldSeats = request.OldSeats,
                NewSeats = dto.NewNumberOfSeats,
                UpdatedAt = DateTime.UtcNow
            }, "Booking updated successfully");
        }
    }
}
