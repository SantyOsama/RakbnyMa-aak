using AutoMapper;
using RakbnyMa_aak.CQRS.BookTripOrchestrator;
using RakbnyMa_aak.CQRS.CreateBooking;
using RakbnyMa_aak.CQRS.Trips.UpdateTrip;
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
            CreateMap<UpdateTripDto, Trip>().ReverseMap();

            CreateMap<CreateBookingDto, Booking>().ReverseMap();
            CreateMap<BookTripDto, Booking>().ReverseMap();
            CreateMap<BookTripDto, CreateBookingDto>().ReverseMap();


        }
    }
}