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

        public List<Tag> GetOrderItems()
        {
            return _tagEntity.OrderByDescending(x => x.CountOfFanfic).Take(10).ToList();
        }

        public int GetItemByName(string name)
        {
            var tag = _tagEntity.SingleOrDefault(x => x.Name == name);
            return tag?.Id ?? -1;
        }

        public int FindOrAdd(string name)
        {
            Tag tag = _tagEntity.SingleOrDefault(x => x.Name == name);
            if(tag != null)
            {
                tag.CountOfFanfic++;
                Save();
                return tag.Id;
            }
            _tagEntity.Add(new Tag { Name = name, CountOfFanfic = 1 });
            Save();
            return _tagEntity.Single(x => x.Name == name).Id;
        }
    }
}
