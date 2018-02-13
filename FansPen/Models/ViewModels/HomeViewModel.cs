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

        public HomeViewModel(List<CategoryViewModel> categorys)
        {
            Fanfics = new List<FanficViewModel>();
            Categorys = categorys;
        }

        public void SetFanficList(List<FanficViewModel> fanfics)
        {
            Fanfics.Clear();
            Fanfics = fanfics;
        }
    }
}
