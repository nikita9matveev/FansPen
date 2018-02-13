using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FansPen.Web.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using FansPen.Web.Models.ViewModels;
using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;

namespace FansPen.Web.Controllers
{
    public class HomeController : Controller
    {
        public FanficRepository FanficRepository;
        public CategoryRepository CategoryRepository;
        public TagRepository TagRepository;

        private HomeViewModel _homeModel;

        public HomeController(ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            CategoryRepository = new CategoryRepository(context);
            TagRepository = new TagRepository(context);
            _homeModel = new HomeViewModel(Mapper.Map<List<CategoryViewModel>>(CategoryRepository.GetList()));
        }

        public IActionResult Index()
        {
            _homeModel.SetList(
                Mapper.Map<List<FanficViewModel>>(FanficRepository.GetAllItems()),
                Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View(_homeModel);
        }

        [HttpGet]
        [Route("Category")]
        public IActionResult Category(string value)
        {
            _homeModel.SetList(
                Mapper.Map<List<FanficViewModel>>(FanficRepository.GetItemByCategory(value)),
                Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View("Index", _homeModel);
        }

        [HttpGet]
        [Route("Tag")]
        public IActionResult Tags(string value)
        {
            _homeModel.SetList(
                Mapper.Map<List<FanficViewModel>>(FanficRepository.GetItemByTags(value)),
                Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View("Index", _homeModel);
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

        public IActionResult Theme(string returnUrl, string value)
        {
            if (Request.Cookies["theme"] == "dark")
            {
                Response.Cookies.Append("theme", "dark");
            }
            else
            {
                Response.Cookies.Append("theme", "light");
            }
            return LocalRedirect(returnUrl);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
