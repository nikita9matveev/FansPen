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
        public List<TagViewModel> Tags { get; set; }

        public HomeViewModel(List<CategoryViewModel> categorys)
        {
            Fanfics = new List<FanficViewModel>();
            Tags = new List<TagViewModel>();
            Categorys = categorys;
        }

        public void SetList(List<FanficViewModel> fanfics, List<TagViewModel> tags)
        {
            Fanfics.Clear();
            foreach(var fanfic in fanfics)
            {
                foreach(var fanTag in fanfic.FanficTags)
                {
                    foreach(var tag in tags)
                    {
                        if(fanTag.TagId == tag.Id)
                        {
                            fanfic.Tags.Add(tag);
                            break;
                        }
                    }
                }
            }
            Fanfics = fanfics;
            Tags.Clear();
            Tags = tags.OrderByDescending(x => x.CountOfFanfic).Take(10).ToList();
        }
    }
}
