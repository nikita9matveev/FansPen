using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<FanficPreViewModel> Fanfics { get; set; }
        public List<CategoryViewModel> Categorys { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public HomeViewModel(List<CategoryViewModel> categorys)
        {
            Fanfics = new List<FanficPreViewModel>();
            Tags = new List<TagViewModel>();
            Categorys = categorys;
        }
    }
}
