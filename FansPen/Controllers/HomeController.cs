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
using System.Threading;
using FansPen.Web.Tools;

namespace FansPen.Web.Controllers
{
    public class HomeController : Controller
    {
        public FanficRepository FanficRepository { get; set; }
        public CategoryRepository CategoryRepository { get; set; }
        public TopicRepository TopicRepository { get; set; }
        public CommentRepository CommentRepository { get; set; }
        public TagRepository TagRepository { get; set; }
        private HomeViewModel _homeModel { get; set; }
        static private List<FanficPreViewModel> _resultFanfics { get; set; }
        private FanficComparer _fanficComparer { get; set; }

        private const int SizeOfPackage = 10;

        public HomeController(ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            CategoryRepository = new CategoryRepository(context);
            TagRepository = new TagRepository(context);
            TopicRepository = new TopicRepository(context);
            CommentRepository = new CommentRepository(context);
            _homeModel = new HomeViewModel(Mapper.Map<List<CategoryViewModel>>(CategoryRepository.GetList()));
            _fanficComparer = new FanficComparer();
        }

        public IActionResult Index()
        {
            _resultFanfics = new List<FanficPreViewModel>();
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            _resultFanfics = Mapper
                .Map<List<FanficPreViewModel>>(FanficRepository.GetNew());
            _resultFanfics.ForEach(x => x.SetTags(tags));
            _homeModel.Fanfics = Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetAllPopular(SizeOfPackage));
            _homeModel.Fanfics.ForEach(x => x.SetTags(tags));
            _homeModel.Tags = tags.OrderByDescending(x => x.CountOfFanfic).Take(20).ToList();
            return View(_homeModel);
        }

        [HttpGet]
        [Route("GetFanfic")]
        public IActionResult GetFanfic(int package)
        {
            List<FanficPreViewModel> fanfics = _resultFanfics.Skip(package).Take(SizeOfPackage).ToList();
            return Json(fanfics);
        }

        [HttpGet]
        [Route("Category")]
        public IActionResult Category(string value = "")
        {
            _resultFanfics = new List<FanficPreViewModel>();
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            _resultFanfics = Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetItemByCategory(value));
            _resultFanfics.ForEach(x => x.SetTags(tags));
            _homeModel.Fanfics = _resultFanfics.Take(SizeOfPackage).ToList();
            _homeModel.Tags = tags.OrderByDescending(x => x.CountOfFanfic).Take(20).ToList();
            return View("Index", _homeModel);
        }

        [HttpGet]
        [Route("Tag")]
        public IActionResult Tags(string value = "")
        {
            _resultFanfics = new List<FanficPreViewModel>();
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            _resultFanfics = Mapper.Map<List<FanficPreViewModel>>(FanficRepository.GetItemByTags(value));
            _resultFanfics.ForEach(x => x.SetTags(tags));
            _homeModel.Fanfics = _resultFanfics.Take(SizeOfPackage).ToList();
            _homeModel.Tags = tags.OrderByDescending(x => x.CountOfFanfic).Take(20).ToList();
            return View("Index", _homeModel);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string value)
        {
            if (value == null) return Redirect("/");
            if(value[0] == '#' && value.Length > 1)
            {
                return RedirectToActionPermanent("Tags","Home", new { value = value.Substring(1, value.Length - 1) });
            }
            _resultFanfics = new List<FanficPreViewModel>();
            List<TagViewModel> tags = Mapper.Map<List<TagViewModel>>(TagRepository.GetList());
            SearchInFanfics(value, tags);
            SearchInComments(value, tags);
            SearchInTopics(value, tags);
            _homeModel.Fanfics = _resultFanfics.Take(SizeOfPackage).ToList();
            _homeModel.Tags = tags.OrderByDescending(x => x.CountOfFanfic).Take(20).ToList();
            return View("Index", _homeModel);
        }

        private void SearchInFanfics(string value, List<TagViewModel> tags)
        {
            List<FanficPreViewModel> resultList = Mapper.Map<List<FanficPreViewModel>>(
                FanficRepository.SearchInFanfics(value));
            resultList.ForEach(x => x.SetTags(tags));
            _resultFanfics.AddRange(resultList);
        }

        public void SearchInComments(string value, List<TagViewModel> tags)
        {
            List<FanficPreViewModel> resultList = Mapper.Map<List<FanficPreViewModel>>(
                CommentRepository.SearchInComments(value));
            resultList.ForEach(x => addToResult(x, tags));
        }

        public void SearchInTopics(string value, List<TagViewModel> tags)
        {
            List<FanficPreViewModel> resultList = Mapper.Map<List<FanficPreViewModel>>(
                TopicRepository.SearchInTopics(value));
            resultList.ForEach(x => addToResult(x, tags));
        }

        private void addToResult(FanficPreViewModel fanfic, List<TagViewModel> tags)
        {
            if(!_resultFanfics.Contains(fanfic, _fanficComparer))
            {
                fanfic.SetTags(tags);
                _resultFanfics.Add(fanfic);
            }
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
