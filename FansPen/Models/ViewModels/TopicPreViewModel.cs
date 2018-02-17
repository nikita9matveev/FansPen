using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ViewModels
{
    public class TopicPreViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public float AverageRating { get; set; }
    }
}
