using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.UserViewModels;
using Social_Media.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class CrudAccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private UserContextEntityFramework _contextUserEF;

        public CrudAccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            UserContextEntityFramework entityFramework)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextUserEF = entityFramework;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel viewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User user = new User {
                    UserName = viewModel.Name,
                    Email = viewModel.Email,
                };

                IdentityResult result = await _contextUserEF.CreateUserAsync(viewModel);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Posts", "PostWall");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                    
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("Register", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel viewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User userContext = await _userManager.FindByIdAsync(viewModel.Id);
                if (userContext != null)
                {
                    userContext.UserName = viewModel.Name;
                    userContext.PhotoProfileRoute = viewModel.PhotoProfileRoute;

                    if (viewModel.DeletePosts != null)
                    {
                        await Task.Run(() => 
                        {
                            foreach (Post post in viewModel.DeletePosts)
                            {
                                userContext.Posts.Remove(post);
                            }
                        });
                        
                    }
                    else if (viewModel.UnfollowUsers != null)
                    {
                        await Task.Run(() =>
                        {
                            foreach (User user in viewModel.UnfollowUsers)
                            {
                                userContext.UserFriends.Remove(user);
                            }
                        });
                    }

                    IdentityResult result = await _contextUserEF.UpdateUserAsync(userContext);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Profile", "Account", new {userId = viewModel.Id});
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
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
            return RedirectToAction("EditUset", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> RemovetUser(string userName, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User userContext = await _userManager.FindByNameAsync(userName);
                if (userContext != null)
                {
                    IdentityResult result = await _contextUserEF.RemovetUserAsync(userContext);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Posts", "PostWall");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
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
            return RedirectToRoute("default");
        }
    }

}
