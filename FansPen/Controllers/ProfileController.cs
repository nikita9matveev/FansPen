using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Web.Controllers
{
    public class ProfileController : Controller
    {
        public ApplicationUserRepository ApplicationUserRepository;
        public FanficRepository FanficRepository;
        public TagRepository TagRepository;
        static private List<FanficPreViewModel> _resultList { get; set; }
        private const int SizeOfPackage = 10;

        public ProfileController(ApplicationContext context)
        {
            ApplicationUserRepository = new ApplicationUserRepository(context);
            FanficRepository = new FanficRepository(context);
            TagRepository = new TagRepository(context);
        }

        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile(string id)
        {
            return View("Index",
                Mapper.Map<ApplicationUserViewModel>(
                    ApplicationUserRepository.GetApplicationUserById(id)
                ));
        }

        

        [HttpGet]
        [Route("GetUserFanfics")]
        public IActionResult GetUserFanfics(string id, string category, int sort)
        {
            _resultList = new List<FanficPreViewModel>();
            _resultList = (category == "All") ?
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetUserFanfics(id, sort)) :
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetUserFanficsByCategory(id, category, sort));
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            _resultList.ForEach(x => x.SetTags(tags));
            return Json(new { fanfics = _resultList.Take(SizeOfPackage) });
        }

        [HttpGet]
        [Route("GetNext")]
        public IActionResult GetNext(int package)
        { 
            return Json(new { fanfics = _resultList.Skip(package).Take(SizeOfPackage) });
        }
    }
}
