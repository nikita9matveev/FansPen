using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingViewModel>();
        }
    }
}
