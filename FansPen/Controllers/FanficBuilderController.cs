using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ScriptModel;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public IActionResult FanficBuilder(string userid = "", int fanficid = -1)
        {
            if (User.Identity.GetUserId() == null)
                return RedirectPermanent("/");
            if (fanficid != -1)
            {
                if (FanficRepository.GetUserIdByFanficId(fanficid) != User.Identity.GetUserId())
                    return RedirectPermanent("/");
            }
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
            foreach (var topicScript in data.Topics) {
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
            foreach (var tag in data.Tags)
            {
                FanficTagRepository.AddNewFanficTag(id, TagRepository.FindOrAdd(tag.Name));
            }
        }

        [HttpPost]
        [Route("DeleteFanfic")]
        public IActionResult DeleteFanfic(int id)
        {
            List<FanficTagViewModel> fanTags = Mapper.Map<List<FanficTagViewModel>>(FanficTagRepository.GetFanficTagByFanficId(id));
            foreach(var fanTag in fanTags)
            {
                TagRepository.SubCountById(fanTag.TagId);
            }
            FanficRepository.DeleteFanfic(id);

            return RedirectPermanent("/");
        }

        [HttpGet]
        [Route("GetFanficById")]
        public IActionResult GetFanficById(int id)
        {
            FanficFullModel fanfic = Mapper.Map<FanficFullModel>(FanficRepository.GetFullById(id));
            fanfic.SetTags(Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return Json(new { fanfic } );
        }

        [HttpPost]
        [Route("EditFanfic")]
        public void EditFanfic([FromBody]FanficScriptModel data)
        {
            FanficFullModel fanfic = Mapper.Map<FanficFullModel>(FanficRepository.EditFanfic(data.Id, data.Name, data.Description, data.ImgUrl));
            foreach (var topic in fanfic.Topics)
            {
                bool isDelete = true;
                foreach (var newTopic in data.Topics)
                {
                    if (topic.Id == newTopic.Id)
                    {
                        isDelete = false;
                        break;
                    }
                }
                if (isDelete)
                {
                    TopicRepository.DeleteTopicById(topic.Id);
                }
            }
            foreach (var topic in data.Topics)
            {
                if (topic.Id != -1)
                {
                    TopicRepository.EditTopic(topic.Id, topic.Number, topic.Name, topic.ImgUrl, topic.Text);
                }
                else
                {
                    Topic newTopic = new Topic
                    {
                        Name = topic.Name,
                        Number = topic.Number,
                        FanficId = data.Id,
                        Text = topic.Text,
                        ImgUrl = topic.ImgUrl,
                        AverageRating = 0
                    };
                    //TopicRepository.AddTopic(newTopic);
                    FanficRepository.AddTopic(fanfic.Id, newTopic);
                }
            }
            FanficRepository.SetAverageRatingById(fanfic.Id);

            List<FanficTagViewModel> fanficTag =
                Mapper.Map<List<FanficTagViewModel>>(FanficTagRepository.GetFanficTagByFanficId(data.Id));
            foreach (var fanTag in fanficTag)
            {
                bool isDelete = true;
                foreach (var newFanTag in data.Tags)
                {
                    if (TagRepository.GetTagNameById(fanTag.TagId) == newFanTag.Name)
                        isDelete = false;
                }
                if (isDelete)
                {
                    FanficTagRepository.DeleteFanficTag(fanTag.FanficId, fanTag.TagId);
                    TagRepository.SubCountById(fanTag.TagId);
                }
            }

            foreach (var tag in data.Tags)
            {
                int idTag = TagRepository.AddOrNull(tag.Name);
                if (FanficTagRepository.FindByFanficIdTagId(fanfic.Id, idTag) == null)
                {
                    FanficTagRepository.AddNewFanficTag(data.Id, idTag);
                    TagRepository.AddCountById(idTag);
                }   
            }
        }
    }
}
