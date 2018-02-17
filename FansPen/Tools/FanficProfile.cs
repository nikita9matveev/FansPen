using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;
using System;

namespace FansPen.Web.Tools
{
    public class FanficProfile : Profile
    {
        public FanficProfile()
        {
            CreateMap<Fanfic, FanficViewModel>()
                .ForMember(dest => dest.AverageRating,
                opt => opt.MapFrom(src => Math.Round(src.AverageRating, 1, MidpointRounding.AwayFromZero)));
        }
    }
}
