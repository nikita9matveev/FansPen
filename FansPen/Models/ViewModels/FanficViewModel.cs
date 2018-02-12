using System;

namespace FansPen.Web.Models.ViewModels
{
    public class FanficViewModel
    {
        public int Id { get; set; }
        public float AverageRating { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditingDate { get; set; }
        public CategoryViewModel Category { get; set; }
        public PreviewUserViewModel ApplicationUser { get; set; }
    }
}
