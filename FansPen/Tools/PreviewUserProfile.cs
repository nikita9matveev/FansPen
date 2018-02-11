using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class PreviewUserProfile : Profile
    {
        public PreviewUserProfile()
        {
            CreateMap<ApplicationUser, PreviewUserViewModel>();
        }
    }
}
