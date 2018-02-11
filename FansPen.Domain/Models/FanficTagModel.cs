using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class FanficTag
    {
        public int Id { get; set; }
        public int FanficId { get; set; }
        public Fanfic Fanfic { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
