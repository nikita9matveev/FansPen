using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Controllers
{
    public class CommentController : Controller
    {
        public CommentRepository CommentRepository;
        public ApplicationUserRepository ApplicationUserRepository;
        public LikeRepository LikeRepository;

        private List<CommentViewModel> _commentViewModels { get; set; }
        private List<PreviewUserViewModel> _previewUserViewModels { get; set; }

        public CommentController(ApplicationContext context)
        {
            CommentRepository = new CommentRepository(context);
            ApplicationUserRepository = new ApplicationUserRepository(context);
            LikeRepository = new LikeRepository(context);
        }

        [HttpGet]
        [Route("GetComments")]
        public IActionResult GetComments(int id, int package)
        {
            _commentViewModels = Mapper.Map<List<CommentViewModel>>(CommentRepository.GetCommentsByIdFanfic(id, package));
            _previewUserViewModels = Mapper.Map<List<PreviewUserViewModel>>(ApplicationUserRepository.GetList());
            List<CommentScriptModel> commentList = new List<CommentScriptModel>();
            foreach (var commentView in _commentViewModels)
            {
                commentList.Add(new CommentScriptModel(commentView, _previewUserViewModels, User.Identity.GetUserId()));
            }
            return Json(commentList);
        }

        [HttpGet]
        [Route("GetNewComments")]
        public IActionResult GetNewComments(int id)
        {
            _commentViewModels = Mapper.Map<List<CommentViewModel>>(CommentRepository.GetNewComments(id));
            _previewUserViewModels = Mapper.Map<List<PreviewUserViewModel>>(ApplicationUserRepository.GetList());
            List<CommentScriptModel> commentList = new List<CommentScriptModel>();
            foreach (var commentView in _commentViewModels)
            {
                commentList.Add(new CommentScriptModel(commentView, _previewUserViewModels, User.Identity.GetUserId()));
            }
            return Json(commentList);
        }

        [HttpPost]
        [Route("SetLike")]
        public IActionResult SetLike(int id, bool isLike)
        {
            int count = -1;
            if (User.Identity.GetUserId() == null)
            {
                return Json(new { count });
            }
            else
            {
                count = isLike? 
                    LikeRepository.RemoveLike(User.Identity.GetUserId(), id) : 
                    LikeRepository.SetLike(User.Identity.GetUserId(), id);
                return Json(new { count });
            }
        }

        [HttpPost]
        [Route("SendComment")]
        public IActionResult SendComment(int id, string text)
        {
            return Json(new { count = CommentRepository.SendComment(User.Identity.GetUserId(), id, text) });
        }
    }
}
