using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Tools
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(dest => dest.RegistrationDate,
                opt => opt.MapFrom(src => src.RegistrationDate.ToShortDateString()))
                .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName == "" || src.FirstName == null ? "NotIndicated" : src.FirstName))
                .ForMember(dest => dest.SecondName,
                opt => opt.MapFrom(src => src.SecondName == "" || src.SecondName == null ? "NotIndicated" : src.SecondName))
                .ForMember(dest => dest.Sex,
                opt => opt.MapFrom(src => src.Sex == "" || src.Sex == null ? "NotIndicated" : src.Sex))
                .ForMember(dest => dest.AboutMe,
                opt => opt.MapFrom(src => src.AboutMe == "" || src.AboutMe == null ? "NotIndicated" : src.AboutMe));
        }
    }
}
