using System.Collections.Generic;

namespace FansPen.Web.Models.ViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public int? FanficId { get; set; }
        public FanficViewModel Fanfic { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public ICollection<RatingViewModel> Ratings { get; set; }
        //public List<PreviewUserViewModel> UsersRated { get; set; }
        public float AverageRating { get; set; }
        public int UserRating { get; set; }
        public TopicViewModel()
        {
            //UsersRated = new List<PreviewUserViewModel>();
            UserRating = 0;
        }
        public void SetUserRating(string userId)
        {
            foreach(var rating in Ratings)
            {
                if(rating.ApplicationUserId == userId)
                {
                    UserRating = rating.Value;
                    break;
                }
            }
        }
    }
}
