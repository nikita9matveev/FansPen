using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Img
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public string ImgUrl { get; set; }
    }
}
