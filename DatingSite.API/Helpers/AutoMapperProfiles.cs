using System.Linq;
using AutoMapper;
using DatingSite.API.Dtos;
using DatingSite.API.Models;

namespace DatingSite.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserProfile>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.BirthdayDate.CalculateAge()));
            CreateMap<User, UserForList>() 
                .ForMember(dest => dest.PhotoUrl , opt => 
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                 .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.BirthdayDate.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreteMap<UserForUpdateDto, User>();
        }
    }
}