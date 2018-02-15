using FansPen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>
    {
        public ApplicationUserRepository(ApplicationContext context) : base(context) { }
    }
}
