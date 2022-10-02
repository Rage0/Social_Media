using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.UserViewModels.PasswordViewModels;
using Social_Media.EntityFramework;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [AllowAnonymous]
    public class PasswordController : Controller
    {
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        public IActionResult ResetPassword(string email)
        {
            return View(new ResetPasswordViewModel
            {
                Email = email
            });
        }
    }
}
