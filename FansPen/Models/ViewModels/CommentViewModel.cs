using System;
using System.Collections.Generic;

namespace FansPen.Web.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public PreviewUserViewModel ApplicationUser { get; set; }
        public ICollection<LikeViewModel> Likes { get; set; }
        public string Text { get; set; }
        public DateTime DataCreate { get; set; }
    }
}
