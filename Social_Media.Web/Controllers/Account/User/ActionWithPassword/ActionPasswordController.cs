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
    public class ActionPasswordController : Controller
    {
        private UserManager<User> _userManager;
        private UserContextEntityFramework _userContextEF;
        public ActionPasswordController(UserManager<User> userManager, UserContextEntityFramework userContextEF)
        {
            _userManager = userManager;
            _userContextEF = userContextEF;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    return RedirectToAction("ResetPassword", "Password", new {email = model.Email});
                }
            }
            return RedirectToAction("ForgotPassword", "Password");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userContextEF.ResetPasswordAsync(user, code, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Posts", "PostWall");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return RedirectToAction("ResetPassword", "Password", new {email = model.Email});
        }
    }
}
