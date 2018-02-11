using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class FanficRepository : BaseRepository<Fanfic>
    {
        private ApplicationContext context;
        private DbSet<Fanfic> fanficEntity;

        public FanficRepository(ApplicationContext context) : base(context) {
            this.context = context;
            fanficEntity = context.Set<Fanfic>();
        }

        public List<Fanfic> GetAllItems()
        {
            return fanficEntity.Include(x => x.Category).Include(x => x.ApplicationUser).ToList();
        }
    }
}