using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Hubs;
using FansPen.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private IHubContext<CommentHub> _commentHubContext { get; set; }

        //private string _currentUserId { get; set; }
        //private bool _isCurrentUserAdmin { get; set; }

        public CommentController(ApplicationContext context, IHubContext<CommentHub> commentHub)
        {
            CommentRepository = new CommentRepository(context);
            ApplicationUserRepository = new ApplicationUserRepository(context);
            LikeRepository = new LikeRepository(context);
            _commentHubContext = commentHub;
            //_currentUserId = User.Identity.GetUserId() ?? "";
            //_isCurrentUserAdmin = User.IsInRole("admin");
        }

        [HttpGet]
        [Route("GetComments")]
        public IActionResult GetComments(int id, int package)
        {
            string userId = User.Identity.GetUserId();
            bool isAdmin = User.IsInRole("admin");
            _commentViewModels = Mapper.Map<List<CommentViewModel>>(CommentRepository.GetCommentsByIdFanfic(id, package));
            _previewUserViewModels = Mapper.Map<List<PreviewUserViewModel>>(ApplicationUserRepository.GetList());
            List<CommentScriptModel> commentList = new List<CommentScriptModel>();
            //_commentViewModels.ForEach(x => commentList.Add(
            //    new CommentScriptModel(x, _previewUserViewModels, _currentUserId, _isCurrentUserAdmin)));
            foreach(var comment in _commentViewModels)
            {
                commentList.Add(new CommentScriptModel(
                    comment,
                    _previewUserViewModels,
                    userId,
                    isAdmin
                    ));
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
            string userId = User.Identity.GetUserId();
            CommentScriptModel newComment = new CommentScriptModel(
                Mapper.Map<CommentViewModel>(CommentRepository.SendComment(userId, id, text)),
                null, userId, false)
            {
                IsYour = false
            };
            var connectionID = HttpContext.Request.Cookies["idClient"];
            _commentHubContext.Clients.AllExcept(connectionID).addMessage(newComment);
            newComment.IsYour = true;
            return Json(new { newComment });
        }

        [HttpPost]
        [Route("DeleteComment")]
        public IActionResult DeleteComment(int idComment, int idFanfic)
        {
            int count = CommentRepository.DeleteComment(idComment, idFanfic);
            var connectionID = HttpContext.Request.Cookies["idClient"];
            _commentHubContext.Clients.AllExcept(connectionID).deleteMessage(idComment, count);
            return Json(new { count });
        }
    }
}
