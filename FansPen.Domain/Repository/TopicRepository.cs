using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class TopicRepository : BaseRepository<Topic>
    {
        private DbSet<Topic> _topicEntity;

        public TopicRepository(ApplicationContext context) : base(context)
        {
            _topicEntity = context.Set<Topic>();
        }

        public Topic GetTopicById(int id)
        {
            return _topicEntity
                .Include(x => x.Fanfic)
                .Include(x => x.Ratings)
                .Include(x => x.Fanfic.Comments)
                .Include(x => x.Fanfic.Topics)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public void SetAverageRatingById(int idTopic, float averageRating)
        {
            Topic topic = _topicEntity.Where(x => x.Id == idTopic).FirstOrDefault();
            if (topic != null)
            {
                topic.AverageRating = averageRating;
                Save();
            }
        }

        public void AddTopic(Topic topic)
        {
            _topicEntity.Add(topic);
            Save();
        }
    }
}
