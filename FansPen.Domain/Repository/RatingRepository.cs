using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class RatingRepository : BaseRepository<Rating>
    {
        private DbSet<Rating> _ratingEntity;

        public RatingRepository(ApplicationContext context) : base(context)
        {
            _ratingEntity = context.Set<Rating>();
        }

        public float SetRating(int idTopic, string idUser, int value)
        {
            Rating rating = _ratingEntity
                .Where(x => x.TopicId == idTopic)
                .Where(x => x.ApplicationUserId == idUser)
                .FirstOrDefault();
            if(rating != null)
            {
                rating.Value = value;
            }
            else
            {
                _ratingEntity.Add(new Rating
                {
                    TopicId = idTopic,
                    Value = value,
                    ApplicationUserId = idUser
                });
            }
            Save();
            float averageTopicRating = 0;
            List<Rating> topicRatings = _ratingEntity.Where(x => x.TopicId == idTopic).ToList();
            foreach(var topicRating in topicRatings)
            {
                averageTopicRating += topicRating.Value;
            }
            return averageTopicRating / topicRatings.Count();
        }

        public int GetCountRatingById(int idTopic)
        {
            return _ratingEntity.Where(x => x.TopicId == idTopic).Count();
        }
    }
}
