using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class FanficPreviewProfile : Profile
    {
        public FanficPreviewProfile()
        {
            CreateMap<Fanfic, FanficPreViewModel>();
        }
    }
}
