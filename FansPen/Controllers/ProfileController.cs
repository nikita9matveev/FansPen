using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FansPen.Web.Controllers
{
    public class ProfileController : Controller
    {
        public ApplicationUserRepository ApplicationUserRepository;

        public FanficRepository FanficRepository;

        public ProfileController(ApplicationContext context)
        {
            ApplicationUserRepository = new ApplicationUserRepository(context);
            FanficRepository = new FanficRepository(context);
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

        [HttpPost]
        [Route("SetFirstName")]
        public void SetFirstName(string id, string value)
        {
            ApplicationUserRepository.SetFirstNameById(id, value);
        }

        [HttpPost]
        [Route("SetSecondName")]
        public void SetSecondName(string id, string value)
        {
            ApplicationUserRepository.SetSecondNameById(id, value);
        }

        [HttpPost]
        [Route("SetSex")]
        public void SetSex(string id, string value)
        {
            ApplicationUserRepository.SetSexById(id, value);
        }

        [HttpPost]
        [Route("SetAboutMe")]
        public void SetAboutMe(string id, string value)
        {
            ApplicationUserRepository.SetAboutMeById(id, value);
        }

        [HttpGet]
        [Route("GetUserFanfics")]
        public IActionResult GetUserFanfics(string id, int package, string category, int sort)
        {
            List<FanficPreViewModel> fanfics = (category == "All") ?
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetUserFanfics(id, sort, package)) :
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetUserFanficsByCategory(id, category, sort, package));
            return Json(new { fanfics });
        }
    }
}
