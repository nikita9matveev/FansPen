using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class FanficRepository : BaseRepository<Fanfic>
    {
        private DbSet<Fanfic> _fanficEntity;
        private TagRepository _tagRepositiry;
        private TagFanficRepository _tagFanficRepository;

        public FanficRepository(ApplicationContext context) : base(context) {
            _fanficEntity = context.Set<Fanfic>();
            _tagRepositiry = new TagRepository(context);
            _tagFanficRepository = new TagFanficRepository(context);
        }

        public List<Fanfic> GetAllItems()
        {
            return _fanficEntity
                .Include(x => x.Category)
                .Include(x => x.ApplicationUser)
                .Include(x => x.FanficTags).ToList();
        }

        public List<Fanfic> GetItemByCategory(string category)
        {
            return _fanficEntity
                .Include(x => x.Category)
                .Include(x => x.ApplicationUser)
                .Include(x => x.FanficTags)
                .Where(x => x.Category.Name == category)
                .OrderByDescending(x => x.CreateDate)
                .ToList();
        }

        public List<Fanfic> GetItemByTags(string tag)
        {
            int tagId = _tagRepositiry.GetItemByName(tag);
            List<FanficTag> tagFanfic = _tagFanficRepository.GetItemByTagId(tagId);
            List<Fanfic> fanficsResult = new List<Fanfic>();
            List<Fanfic> fanficsList = _fanficEntity.Include(x => x.Category)
                .Include(x => x.ApplicationUser).Include(x => x.FanficTags).ToList();
            foreach (var tagFan in tagFanfic)
            {
                foreach (var fan in fanficsList)
                {
                    if (tagFan.FanficId == fan.Id)
                    {
                        fanficsResult.Add(fan);
                        break;
                    }
                }
            }
            return fanficsResult.OrderByDescending(x => x.CreateDate).ToList();
        }

        public Fanfic GetById(int id)
        {
            return _fanficEntity
                .Include(x => x.ApplicationUser)
                .Include(x => x.Category)
                .Include(x => x.Topics)
                .Include(x => x.FanficTags)
                .Include(x => x.Comments)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public void SetAverageRatingById(int id)
        {
            Fanfic fanfic = _fanficEntity
                .Include(x => x.Topics)
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if(fanfic != null)
            {
                float averageFanficRating = 0;
                foreach(var topic in fanfic.Topics)
                {
                    averageFanficRating += topic.AverageRating;
                }
                fanfic.AverageRating = averageFanficRating / fanfic.Topics.Count();
                Save();
            }
        }

        public List<Fanfic> GetUserFanficsByCategory(string idUser, string category, int sort, int package)
        {
            if(sort == 0)
            {
                return _fanficEntity
                .Include(x => x.Category)
                .Where(x => x.ApplicationUserId == idUser)
                .Where(x => x.Category.Name == category)
                .OrderByDescending(x => x.CreateDate)
                .Skip(package)
                .Take(10).ToList();
            }
            else
            {
                return _fanficEntity
                .Include(x => x.Category)
                .Where(x => x.ApplicationUserId == idUser)
                .Where(x => x.Category.Name == category)
                .OrderByDescending(x => x.AverageRating)
                .Skip(package)
                .Take(10).ToList();
            }
        }

        public List<Fanfic> GetUserFanfics(string idUser, int sort, int package)
        {
            if (sort == 0)
            {
                return _fanficEntity
                .Include(x => x.Category)
                .Where(x => x.ApplicationUserId == idUser)
                .OrderByDescending(x => x.CreateDate)
                .Skip(package)
                .Take(10).ToList();
            }
            else
            {
                return _fanficEntity
                .Include(x => x.Category)
                .Where(x => x.ApplicationUserId == idUser)
                .OrderByDescending(x => x.AverageRating)
                .Skip(package)
                .Take(10).ToList();
            }
        }

        public int AddFanfic(Fanfic fanfic)
        {
            _fanficEntity.Add(fanfic);
            Save();
            return _fanficEntity.Where(x => x.ImgUrl == fanfic.ImgUrl).First().Id;
        }
    }
}