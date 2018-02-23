using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Tools
{
    public class FanficFullProfile : Profile
    {
        public FanficFullProfile()
        {
            CreateMap<Fanfic, FanficFullModel>();
        }
    }
}
