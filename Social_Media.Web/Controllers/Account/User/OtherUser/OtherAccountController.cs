using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.UserViewModels;
using Social_Media.EntityFramework;
using Social_Media.Web.Model.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
    public class OtherAccountController : Controller
    {
        private UserContextEntityFramework _userContextEF;
        public OtherAccountController(UserContextEntityFramework userContextEF)
        {
            _userContextEF = userContextEF;
        }

        public async Task<IActionResult> OtherProfile(string userName)
        {
            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.UserFriends)
                .FirstOrDefaultAsync(user => user.UserName == userName);

            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("AllUsers");
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


        public async Task<IActionResult> AllUsers()
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
    }   
}
