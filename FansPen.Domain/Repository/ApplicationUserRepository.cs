using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>
    {
        private DbSet<ApplicationUser> _applicationUserEntity;

        public ApplicationUserRepository(ApplicationContext context) : base(context)
        {
            _applicationUserEntity = context.Set<ApplicationUser>();
        }

        public ApplicationUser GetApplicationUserById(string id)
        {
            return _applicationUserEntity
                .Include(x => x.Fanfics)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}
