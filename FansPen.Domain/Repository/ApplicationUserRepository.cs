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

        public void SetFirstNameById(string id, string firstName)
        {
            ApplicationUser user = _applicationUserEntity
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if(user != null)
            {
                user.FirstName = firstName;
                Save();
            }
        }

        public void SetSecondNameById(string id, string secondName)
        {
            ApplicationUser user = _applicationUserEntity
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (user != null)
            {
                user.SecondName = secondName;
                Save();
            }
        }

        public void SetSexById(string id, string sex)
        {
            ApplicationUser user = _applicationUserEntity
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (user != null)
            {
                user.Sex = sex;
                Save();
            }
        }

        public void SetAboutMeById(string id, string aboutMe)
        {
            ApplicationUser user = _applicationUserEntity
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (user != null)
            {
                user.AboutMe = aboutMe;
                Save();
            }
        }
    }
}
