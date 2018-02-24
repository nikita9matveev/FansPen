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
using Nest;
using Elasticsearch.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetAllPopular()),
                Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View(_homeModel);
        }

        [HttpGet]
        [Route("GetFanfic")]
        public IActionResult GetFanfic(int package)
        {
            List<FanficPreViewModel> fanfics = Mapper
                .Map<List<FanficPreViewModel>>(FanficRepository.GetNew(package));
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            fanfics.ForEach(x => x.SetTags(tags));
            return Json(fanfics);
        }

        [HttpGet]
        [Route("Category")]
        public IActionResult Category(string value = "")
        {
            _homeModel.SetList(
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetItemByCategory(value, 0)),
                Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View("Index", _homeModel);
        }

        [HttpGet]
        [Route("GetFanficCategory")]
        public IActionResult GetFanficCategory(string value, int package)
        {
            List<FanficPreViewModel> fanfics = Mapper
                .Map<List<FanficPreViewModel>>(FanficRepository.GetItemByCategory(value, package));
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            fanfics.ForEach(x => x.SetTags(tags));
            return Json(fanfics);
        }

        [HttpGet]
        [Route("Tag")]
        public IActionResult Tags(string value = "")
        {
            _homeModel.SetList(
                Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetItemByTags(value, 0)),
                Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View("Index", _homeModel);
        }

        [HttpGet]
        [Route("GetFanficTag")]
        public IActionResult GetFanficTag(string value, int package)
        {
            List<FanficPreViewModel> fanfics = Mapper
                .Map<List<FanficPreViewModel>>(FanficRepository.GetItemByTags(value, package));
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            fanfics.ForEach(x => x.SetTags(tags));
            return Json(fanfics);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string value = "")
        {
            if(value[0] == '#' && value.Length > 1)
            {
                return RedirectToActionPermanent("Tags","Home", new { value = value.Substring(1, value.Length - 1) });
            }
            

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
            return RedirectPermanent(returnUrl);
        }

        public IActionResult Theme(string returnUrl)
        {
            if (HttpContext.Request.Cookies["theme"] == "dark")
            {
                HttpContext.Response.Cookies.Append(
                    "theme", "light",
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            }
            else
            {
                HttpContext.Response.Cookies.Append(
                    "theme", "dark",
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            }
            return RedirectPermanent(returnUrl);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
