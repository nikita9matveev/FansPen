using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public float AverageRating { get; set; }
        public Topic()
        {
            Ratings = new List<Rating>();
        }
    }
}
