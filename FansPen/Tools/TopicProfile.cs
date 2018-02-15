using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic, TopicViewModel>();
        }
    }
}
