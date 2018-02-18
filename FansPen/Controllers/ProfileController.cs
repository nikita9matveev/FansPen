using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FansPen.Web.Controllers
{
    public class ProfileController : Controller
    {
        public ApplicationUserRepository ApplicationUserRepository;

        public ProfileController(ApplicationContext context)
        {
            ApplicationUserRepository = new ApplicationUserRepository(context);
        }

        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile(string id)
        {
            return View("Index");
        }
    }
}
