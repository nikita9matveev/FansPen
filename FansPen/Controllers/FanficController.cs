using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FansPen.Web.Controllers
{
    public class FanficController : Controller
    {
        public FanficRepository FanficRepository;
        public TagRepository TagRepository;

        private FanficViewModel _fanficViewModel { get; set; }

        public FanficController(ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            TagRepository = new TagRepository(context);
        }

        [HttpGet]
        [Route("Fanfic")]
        public IActionResult Fanfic(int id)
        {
            _fanficViewModel = Mapper.Map<FanficViewModel>(FanficRepository.GetById(id));
            if (_fanficViewModel == null) return LocalRedirect("/");
            _fanficViewModel.SetTags(Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View("Index", _fanficViewModel);
        }

    }
}
