using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountOfFanfic { get; set; }
        public virtual ICollection<FanficTag> FanficTags { get; set; }
        public Tag()
        {
            FanficTags = new List<FanficTag>();
        }
    }
}
