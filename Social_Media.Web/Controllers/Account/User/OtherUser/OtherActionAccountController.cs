using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Model.UserModel;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
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
        public async Task<IActionResult> AddFriendToUser(FriendNameAndUserName model, string returnUrl = "")
        {
            User userFriend = await _userContextEF.GetAllUsers()
                .FirstOrDefaultAsync(user => user.UserName == model.FriendName);

            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.UserFriends)
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
    }
}
