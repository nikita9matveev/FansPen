using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;
using System;

namespace FansPen.Web.Tools
{
    public class FanficPreviewProfile : Profile
    {
        public FanficPreviewProfile()
        {
            CreateMap<Fanfic, FanficPreViewModel>()
                .ForMember(dest => dest.AverageRating,
                opt => opt.MapFrom(src => Math.Round(src.AverageRating, 1, MidpointRounding.AwayFromZero)))
                .ForMember(dest => dest.CreateDate,
                opt => opt.MapFrom(src => src.CreateDate.ToShortDateString()));
        }
    }
}
