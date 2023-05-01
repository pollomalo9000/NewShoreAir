using AutoMapper;

using NewShoreAir.Domain.Dtos;

namespace NewShoreAir.Domain.Automapper
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<FlightDto, Flight>()
                    .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.DepartureStation))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.ArrivalStation))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.Transport, opt => opt.MapFrom(src => new Transport
                    {
                        FlightCarrier = src.FlightCarrier,
                        FlightNumber = src.FlightNumber
                    }));





        }
    }
}