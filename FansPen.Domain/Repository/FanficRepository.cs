﻿using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class FanficRepository : BaseRepository<Fanfic>
    {
        private ApplicationContext _context;
        private DbSet<Fanfic> _fanficEntity;
        private TagRepository _tagRepositiry;
        private TagFanficRepository _tagFanficRepository;

        public FanficRepository(ApplicationContext context) : base(context) {
            _context = context;
            _fanficEntity = context.Set<Fanfic>();
            _tagRepositiry = new TagRepository(context);
            _tagFanficRepository = new TagFanficRepository(context);
        }

        public List<Fanfic> GetAllItems()
        {
            return _fanficEntity.Include(x => x.Category).Include(x => x.ApplicationUser).ToList();
        }

        public List<Fanfic> GetItemByCategory(string category)
        {
            return _fanficEntity.Include(x => x.Category)
                .Include(x => x.ApplicationUser)
                .Where(x => x.Category.Name == category).ToList();
        }

        public List<Fanfic> GetItemByTags(string tag)
        {
            int tagId = _tagRepositiry.GetItemByName(tag);
            List<FanficTag> tagFanfic = _tagFanficRepository.GetItemByTagId(tagId);
            List<Fanfic> fanficsResult = new List<Fanfic>();
            List<Fanfic> fanficsList = _fanficEntity.Include(x => x.Category)
                .Include(x => x.ApplicationUser).ToList();
            foreach (var tagFan in tagFanfic)
            {
                foreach (var fan in fanficsList)
                {
                    if (tagFan.FanficId == fan.Id)
                    {
                        fanficsResult.Add(fan);
                    }
                }
            }
            return fanficsResult;
        }
    }
}