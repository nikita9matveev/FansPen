using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class ImgProfile : Profile
    {
        public ImgProfile()
        {
            CreateMap<Img, ImgViewModel>();
        }
    }
}
