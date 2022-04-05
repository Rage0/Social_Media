using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.Web.Models.UserViewModels;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        public IActionResult EditUser(string userId)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(model.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl) || string.IsNullOrWhiteSpace(model.ReturnUrl))
                        {
                            return RedirectToAction("Posts", "PostWall");
                        }
                        else
                        {
                            return RedirectToRoute(model.ReturnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password don't match");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email not registred");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Posts", "PostWall");
        }
    }
}
