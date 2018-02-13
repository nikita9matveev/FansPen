using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class TagRepository : BaseRepository<Tag>
    {
        private DbSet<Tag> _tagEntity;

        public TagRepository(ApplicationContext context) : base(context)
        {
            _tagEntity = context.Set<Tag>();
        }

        public int GetItemByName(string name)
        {
            var tag = _tagEntity.SingleOrDefault(x => x.Name == name);
            return tag?.Id ?? -1;
        }
    }
}
