using AutoMapper;
using RakbnyMa_aak.CQRS.BookTripOrchestrator;
using RakbnyMa_aak.CQRS.CreateBooking;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RakbnyMa_aak.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RegisterUserDto, ApplicationUser>().ReverseMap();
            CreateMap<TripDto, Trip>().ReverseMap();
            CreateMap<BookTripDto, BookTripCommand>()
                .ForMember(dest => dest.TripDetails, opt => opt.MapFrom(src => src));
            CreateMap<CreateBookingDto, Booking>()
               .ForMember(dest => dest.BookingDate, opt => opt.Ignore()) // we'll set this manually
               .ForMember(dest => dest.RequestStatus, opt => opt.Ignore());
            CreateMap<BookTripDto, Booking>()
              .ForMember(dest => dest.BookingDate, opt => opt.Ignore())
              .ForMember(dest => dest.RequestStatus, opt => opt.Ignore());


        }
    }
}