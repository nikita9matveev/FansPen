using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagViewModel>();
        }
    }
}
