using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FansPen.Web.Controllers
{
    public class FanficController : Controller
    {
        [HttpGet]
        [Route("Fanfic")]
        public IActionResult Fanfic(int id)
        {
            ViewData["id"] = id;
            return View("Index");
        }
    }
}
