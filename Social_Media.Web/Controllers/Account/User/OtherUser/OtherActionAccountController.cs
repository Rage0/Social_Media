using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Model.UserModel;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
    public class OtherActionAccountController : Controller
    {
        private UserContextEntityFramework _userContextEF;
        private UserManager<User> _userManager;
        public OtherActionAccountController(UserContextEntityFramework userContextEF, UserManager<User> userManager)
        {
            _userContextEF = userContextEF;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddFollowToUser(FriendNameAndUserName model, string returnUrl = "")
        {
            User userFriend = await _userContextEF.GetAllUsers()
                .Include(user => user.FollowingUser)
                .FirstOrDefaultAsync(user => user.UserName == model.FriendName);

            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.UserFriends)
                .FirstOrDefaultAsync(user => user.UserName == model.UserName);

            if (userFriend != null && user != null)
            {
                user.UserFriends.Add(userFriend);
                userFriend.FollowingUser.Add(user);

                await _userContextEF.UpdateUserAsync(user);

                if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("MyProfile", "Account", new { userName = model.UserName });
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction("MyProfile", "Account", new { userName = model.UserName });
        }

        [HttpPost]
        public async Task<IActionResult> AddFriendToUser(FriendNameAndUserName model, string returnUrl = "")
        {
            User userFriend = await _userContextEF.GetAllUsers()
                .Include(user => user.FollowingUser)
                .FirstOrDefaultAsync(user => user.UserName == model.FriendName);

            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.UserFriends)
                .Include(user => user.FollowingUser)
                .FirstOrDefaultAsync(user => user.UserName == model.UserName);

            if (userFriend != null && user != null)
            {
                user.UserFriends.Add(userFriend);

                await _userContextEF.UpdateUserAsync(user);

                if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("MyProfile", "Account", new { userName = model.UserName });
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction("MyProfile", "Account", new { userName = model.UserName });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFriend(string friendName, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User user = await _userContextEF
                    .GetAllUsers()
                    .Include(user => user.UserFriends)
                    .Include(user => user.FollowingUser)
                    .FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);

                User userFriend = await _userContextEF
                    .GetAllUsers()
                    .Include(user => user.UserFriends)
                    .FirstOrDefaultAsync(user => user.UserName == friendName);

                if (user == null && userFriend == null)
                {
                    return RedirectToAction("MyProfile", "Account", new { userName = User.Identity.Name });
                }

                if (user.UserFriends.Contains(userFriend))
                {
                    user.UserFriends.Remove(userFriend);

                    await _userContextEF.UpdateUserAsync(user);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("MyFriends", "OtherAccount", new { userName = User.Identity.Name });
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("MyProfile", "Account", new { userName = User.Identity.Name });
        }

    }
}
