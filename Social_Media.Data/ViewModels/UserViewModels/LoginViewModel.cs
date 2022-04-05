using System.ComponentModel.DataAnnotations;

namespace Social_Media.Web.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }   
    }
}
