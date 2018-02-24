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

        public void SetList(List<FanficPreViewModel> fanfics, List<TagViewModel> tags)
        {
            Fanfics.Clear();
            fanfics.ForEach(x => x.SetTags(tags));
            Fanfics = fanfics;
            Tags.Clear();
            Tags = tags.OrderByDescending(x => x.CountOfFanfic).Take(10).ToList();
        }
    }
}
