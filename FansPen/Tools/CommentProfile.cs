using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;

namespace FansPen.Web.Tools
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentViewModel>();
        }
    }
}
