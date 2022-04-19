using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.UserViewModels;
using Social_Media.EntityFramework;

using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private UserContextEntityFramework _userContextEF;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            UserContextEntityFramework userContextEF)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userContextEF = userContextEF;
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> AllUsersAsync()
        {
            User user = await _userContextEF
                .GetAllUsers()
                .Include(userInclude => userInclude.UserFriends)
                .FirstOrDefaultAsync(user => user.UserName == HttpContext.User.Identity.Name);

            return View(new UserAndAllUsersViewModel
            {
                UsersFromContext = _userContextEF.GetAllUsers()
                .Where(userContext =>
                       userContext.UserName != user.UserName &&
                       !user.UserFriends.Contains(userContext))
                .ToList(),

                User = user
            });
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
                            return Redirect(model.ReturnUrl);
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

        public async Task<IActionResult> MyProfile(string userName)
        {
            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.Posts)
                .Include(user => user.UserFriends)
                .Include(user => user.FollowingUser)
                .FirstOrDefaultAsync(user => user.UserName == userName);

            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> MyFriends(string userName)
        {
            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.UserFriends)
                    .ThenInclude(userAnother => userAnother.PrivateChats)
                .Include(user => user.PrivateChats)
                .FirstOrDefaultAsync(user => user.UserName == userName);

            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Posts", "PostWall");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Posts", "PostWall");
        }
    }
}
