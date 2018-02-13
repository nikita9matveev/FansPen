using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class ApplicationUserTopicProfile : Profile
    {
        public ApplicationUserTopicProfile()
        {
            CreateMap<ApplicationUserTopic, ApplicationUserTopicViewModel>();
        }
    }
}
