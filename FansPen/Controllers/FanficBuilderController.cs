using FansPen.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FansPen.Web.Controllers
{
    public class FanficBuilderController : Controller
    {
        public FanficBuilderController(ApplicationContext context)
        {

        }

        [HttpGet]
        [Route("FanficBuilder")]
        public IActionResult FanficBuilder()
        {
            if (User.Identity.GetUserId() == null)
                return RedirectPermanent("/");
            return View("Index");
        }
    }
}
