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

        public void AddNewFanficTag(int idFanfic, int idTag)
        {
            _fanficTagEntity.Add(new FanficTag { FanficId = idFanfic, TagId = idTag });
            Save();
        }
    }
}
