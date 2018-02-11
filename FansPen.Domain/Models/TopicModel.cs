using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public int? FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public ICollection<Img> Imgs { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<ApplicationUserTopic> WhoRated { get; set; }
        public float AverageRating { get; set; }
        public Topic()
        {
            Imgs = new List<Img>();
            Ratings = new List<Rating>();
            WhoRated = new List<ApplicationUserTopic>();
        }
    }
}
