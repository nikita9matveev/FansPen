using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class TagFanficRepository : BaseRepository<FanficTag>
    {
        private DbSet<FanficTag> _fanficTagEntity;

        public TagFanficRepository(ApplicationContext context) : base(context)
        {
            _fanficTagEntity = context.Set<FanficTag>();
        }

        public List<FanficTag> GetItemByTagId(int id)
        {
            return _fanficTagEntity.Where(x => x.TagId == id).ToList();
        }
    }
}
