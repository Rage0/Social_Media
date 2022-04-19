using System.ComponentModel.DataAnnotations;

namespace Social_Media.Data.ViewModels.UserViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string PasswordConfirm { get; set; }
    }
}
