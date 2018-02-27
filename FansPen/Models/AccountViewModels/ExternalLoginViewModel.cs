using System.ComponentModel.DataAnnotations;

namespace FansPen.Web.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
