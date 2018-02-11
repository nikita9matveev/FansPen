using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<FanficViewModel> Fanfics { get; set; }
        public List<CategoryViewModel> Categorys { get; set; }
        //public List<TagViewModel> Tags { get; set; }
        public HomeViewModel(List<FanficViewModel> fans, List<CategoryViewModel> categorys)//, List<TagViewModel> tags)
        {
            Fanfics = fans;
            Categorys = categorys;
            //Tags = tags;
        }
    }
}
