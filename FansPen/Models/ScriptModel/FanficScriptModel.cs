using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ScriptModel
{
    public class FanficScriptModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public IList<TopicScriptModel> Topics { get; set; }
        public IList<TagScriptModel> Tags { get; set; }
    }
}
