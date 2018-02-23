using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class FanficTagRepository : BaseRepository<FanficTag>
    {
        private DbSet<FanficTag> _fanficTagEntity;

        public FanficTagRepository(ApplicationContext context) : base(context)
        {
            _fanficTagEntity = context.Set<FanficTag>();
        }

        public List<FanficTag> GetItemByTagId(int id)
        {
            return _fanficTagEntity.Where(x => x.TagId == id).ToList();
        }

        public void AddNewFanficTag(int idFanfic, int idTag)
        {
            if (_fanficTagEntity
                .Where(x => x.FanficId == idFanfic)
                .Where(x => x.TagId == idTag)
                .FirstOrDefault() == null)
            {
                _fanficTagEntity.Add(new FanficTag { FanficId = idFanfic, TagId = idTag });
                Save();
            }
        }

        public List<FanficTag> GetFanficTagByFanficId(int id)
        {
            return _fanficTagEntity.Where(x => x.FanficId == id).ToList();
        }

        public void DeleteFanficTag(int fanficId, int tagId)
        {
            FanficTag fanficTag = _fanficTagEntity
                .Where(x => x.FanficId == fanficId)
                .Where(x => x.TagId == tagId).First();
            if(fanficTag != null)
            {
                _fanficTagEntity.Remove(fanficTag);
                Save();
            }
        }

        public FanficTag FindByFanficIdTagId(int fanficId, int tagId)
        {
            return _fanficTagEntity
                .Where(x => x.FanficId == fanficId)
                .Where(x => x.TagId == tagId).FirstOrDefault();
        }
    }
}
