using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string Interests { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "About me")]
        public string AboutMe { get; set; }

        public string StatusMessage { get; set; }
    }
}
