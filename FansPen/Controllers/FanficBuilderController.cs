using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ScriptModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FansPen.Web.Controllers
{
    public class FanficBuilderController : Controller
    {
        public FanficRepository FanficRepository;
        public TopicRepository TopicRepository;
        public TagRepository TagRepository;
        public FanficTagRepository FanficTagRepository;

        public FanficBuilderController(ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            TopicRepository = new TopicRepository(context);
            FanficTagRepository = new FanficTagRepository(context);
            TagRepository = new TagRepository(context);
        }

        [HttpGet]
        [Route("FanficBuilder")]
        public IActionResult FanficBuilder()
        {
            if (User.Identity.GetUserId() == null)
                return RedirectPermanent("/");
            return View("Index");
        }

        [HttpPost]
        [Route("CreateFanfic")]
        public void CreateFanfic([FromBody]FanficScriptModel data)
        {
            Fanfic fanfic = new Fanfic
            {
                ApplicationUserId = User.Identity.GetUserId(),
                AverageRating = 0,
                CategoryId = data.Category,
                CreateDate = DateTime.Now,
                Description = data.Description,
                ImgUrl = data.ImgUrl,
                Name = data.Name
            };
            int id = FanficRepository.AddFanfic(fanfic);
            foreach(var topicScript in data.Topics){
                Topic topic = new Topic
                {
                    Name = topicScript.Name,
                    Number = topicScript.Number,
                    FanficId = id,
                    Text = topicScript.Text,
                    ImgUrl = topicScript.ImgUrl,
                    AverageRating = 0
                };
                TopicRepository.AddTopic(topic);
            }
            foreach(var tag in data.Tags)
            {
                FanficTagRepository.AddNewFanficTag(id, TagRepository.FindOrAdd(tag.Name));
            }
        }
    }
}
