using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreateBookingCommandHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request.BookingDto);
            booking.BookingDate = DateTime.UtcNow;
            booking.RequestStatus = RequestStatus.Pending;

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(booking.Id, "Trip has been created successfully.");
        }

    }
}
