using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, LikeViewModel>();
        }
    }
}
