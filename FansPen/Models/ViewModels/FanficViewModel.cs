using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ViewModels
{
    public class FanficViewModel
    {
        public int Id { get; set; }
        public float AverageRating { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditingDate { get; set; }
        public PreviewUserViewModel ApplicationUser { get; set; }
        public CategoryViewModel Category { get; set; }
        public virtual ICollection<CommentViewModel> Comments { get; set; }
        public virtual ICollection<TopicPreViewModel> Topics { get; set; }
        public virtual ICollection<FanficTagViewModel> FanficTags { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public void SetTags(List<TagViewModel> tags)
        {
            Tags = new List<TagViewModel>();
            foreach (var fanTag in FanficTags)
            {
                foreach (var tag in tags)
                {
                    if (fanTag.TagId == tag.Id)
                    {
                        Tags.Add(tag);
                        break;
                    }
                }
            }
        }
    }
}
