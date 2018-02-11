using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Like> Likes { get; set; }
        public string Text { get; set; }
        public DateTime DataCreate { get; set; }
        public Comment()
        {
            Likes = new List<Like>();
        }
    }
}
