using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Fanfic
    {
        public int Id { get; set; }
        public float AverageRating { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditingDate { get; set; }       
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<FanficTag> FanficTags { get; set; }
        public Fanfic()
        {
            Comments = new List<Comment>();
            Topics = new List<Topic>();
            FanficTags = new List<FanficTag>();
        }
    }
}
