using AutoMapper;
using RakbnyMa_aak.CQRS.Features.Booking.Orchestrators.BookTripRequest;
using RakbnyMa_aak.CQRS.Features.Cities;
using RakbnyMa_aak.CQRS.Features.Governorates;
using RakbnyMa_aak.CQRS.Queries.GetMessagesByTripId;
using RakbnyMa_aak.DTOs.Auth.RequestDTOs;
using RakbnyMa_aak.DTOs.BookingsDTOs.Requests;
using RakbnyMa_aak.DTOs.DriverDTOs.ResponseDTOs;
using RakbnyMa_aak.DTOs.TripDTOs.RequestsDTOs;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.Models;
using static RakbnyMa_aak.Utilities.Enums;
namespace RakbnyMa_aak.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RegisterUserRequestDto, ApplicationUser>().ReverseMap();
            CreateMap<TripRequestDto, Trip>().ReverseMap();
            CreateMap<CreateBookingRequestDto, Booking>().ReverseMap();
            CreateMap<BookTripDto, Booking>().ReverseMap();
            CreateMap<BookTripDto, CreateBookingRequestDto>().ReverseMap();
            CreateMap<GovernorateDto, Governorate>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<Message, MessageDto>();
            CreateMap<City, CityResponseDTO>()
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name));
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name));
            CreateMap<Governorate, GovernorateResponseDTO>();
            CreateMap<GovernorateDto, Governorate>();

            CreateMap<ApplicationUser, UserResponseDto>()
                .ForMember(dest => dest.TotalTrips,
                    opt => opt.MapFrom(src => src.Bookings
                        .Count(b => b.RequestStatus == RequestStatus.Confirmed)))

                .ForMember(dest => dest.AverageRating,
                    opt => opt.MapFrom(src =>
                        src.RatingsReceived != null && src.RatingsReceived.Any()
                            ? src.RatingsReceived.Average(r => r.RatingValue)
                            : 0));


            CreateMap<ApplicationUser, DriverResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))

            .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Driver.CarModel))
            .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => src.Driver.CarType))
            .ForMember(dest => dest.CarColor, opt => opt.MapFrom(src => src.Driver.CarColor))
            .ForMember(dest => dest.CarCapacity, opt => opt.MapFrom(src => src.Driver.CarCapacity))

            .ForMember(dest => dest.TotalTrips, opt => opt.MapFrom(src =>
                src.Driver.Trips != null ? src.Driver.Trips.Count : 0))

            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                src.RatingsReceived != null && src.RatingsReceived.Any()
                    ? src.RatingsReceived.Average(r => r.RatingValue)
                    : 0));

        }
    }
}