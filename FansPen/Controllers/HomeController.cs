using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FansPen.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace FansPen.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string returnUrl)
        {
            string culture = CultureInfo.CurrentCulture.Name == "ru" ? "en" : "ru";
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        public IActionResult Theme(string returnUrl)
        {
            if (Request.Cookies["theme"] == null)
            {
                Response.Cookies.Append("theme", "light");
            }
            else
            {
                if (Request.Cookies["theme"] == "light")
                {
                    Response.Cookies.Append("theme", "dark");
                }
                else if (Request.Cookies["theme"] == "dark")
                {
                    Response.Cookies.Append("theme", "light");
                }
            }
            return LocalRedirect(returnUrl);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
