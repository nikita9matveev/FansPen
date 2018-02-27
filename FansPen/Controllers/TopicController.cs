using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FansPen.Web.Controllers
{
    public class TopicController : Controller
    {
        public RatingRepository RatingRepository;
        public TopicRepository TopicRepository;
        public FanficRepository FanficRepository;

        public TopicController(ApplicationContext context)
        {
            TopicRepository = new TopicRepository(context);
            RatingRepository = new RatingRepository(context);
            FanficRepository = new FanficRepository(context);
        }

        [HttpGet]
        [Route("Topic")]
        public IActionResult Topic(int id, string mode = "default")
        {
            TopicViewModel topicView = Mapper.Map<TopicViewModel>(TopicRepository.GetTopicById(id));
            topicView.SetUserRating(User.Identity.GetUserId());
            return View(topicView);
        }

        [HttpPost]
        [Route("SetRating")]
        public IActionResult Topic(int rating, int idFanfic, int idTopic)
        {
            float averageTopicRating = RatingRepository.SetRating(idTopic, User.Identity.GetUserId(), rating);
            TopicRepository.SetAverageRatingById(idTopic, averageTopicRating);
            FanficRepository.SetAverageRatingById(idFanfic);
            averageTopicRating = (float)Math.Round(averageTopicRating, 1);
            int countRatings = RatingRepository.GetCountRatingById(idTopic);
            return Json(new { averageTopicRating, countRatings});
        }
    }
}
