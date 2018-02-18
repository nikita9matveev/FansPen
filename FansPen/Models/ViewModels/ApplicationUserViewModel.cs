using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string AboutMe { get; set; }
        public string Sex { get; set; }
        public string AvatarUrl { get; set; }
        public string ProviderKey { get; set; }
        public ICollection<FanficPreViewModel> Fanfics { get; set; }
    }
}
