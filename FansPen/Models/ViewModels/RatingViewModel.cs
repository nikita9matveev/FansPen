using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int? TopicId { get; set; }
        public int Value { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
