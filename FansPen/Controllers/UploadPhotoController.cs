using System.Collections.Generic;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FansPen.Web.Controllers
{
    public class UploadPhotoController : Controller
    {
        public ApplicationUserRepository ApplicationUserRepository;

        public UploadPhotoController(ApplicationContext context)
        {
            ApplicationUserRepository = new ApplicationUserRepository(context);
        }

        [HttpPost]
        [Route("UploadPhoto")]
        public void UploadPhoto(string id, string avatarUrl)
        {
            ApplicationUserRepository.UploadAvatar(id, avatarUrl);
        }
    }
}
