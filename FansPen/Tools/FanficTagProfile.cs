using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class FanficTagProfile : Profile
    {
        public FanficTagProfile()
        {
            CreateMap<FanficTag, FanficTagViewModel>();
        }
    }
}
