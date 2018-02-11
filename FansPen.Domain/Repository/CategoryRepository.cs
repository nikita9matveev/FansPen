using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(ApplicationContext context) : base(context) { }
    }
}
