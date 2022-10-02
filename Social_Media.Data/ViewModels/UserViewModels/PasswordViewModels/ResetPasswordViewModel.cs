using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Data.ViewModels.UserViewModels.PasswordViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
