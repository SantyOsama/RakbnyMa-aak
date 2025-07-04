using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.SignalR;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;


        public CreateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHubContext<NotificationHub> hubContext)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<Response<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.BookingDto);

            var tripPrice = await _unitOfWork.TripRepository
                    .GetAllQueryable()
                    .Where(t => t.Id == booking.TripId)
                    .Select(t => t.PricePerSeat)
                    .FirstOrDefaultAsync();

            if (tripPrice == default)
                return Response<int>.Fail("Trip not found.");

            booking.TotalPrice = tripPrice * booking.NumberOfSeats;

            await _unitOfWork.BookingRepository.AddAsync(booking);

            await _unitOfWork.CompleteAsync();

            //Notification but i will do orchestrator

            return Response<int>.Success(booking.Id, "Booking request sent to driver.");
        }

    }
}
