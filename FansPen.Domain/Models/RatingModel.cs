using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public int Value { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
