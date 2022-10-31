using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.UserViewModels;
using Social_Media.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private UserContextEntityFramework _userContextEF;

        public AdminController(RoleManager<IdentityRole> roleManager,
            IRepositoryEntityFramework entityFramework,
            UserManager<User> userManager,
            UserContextEntityFramework userContextEF)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextEF = entityFramework;
            _userContextEF = userContextEF;
        }

        public IActionResult Posts()
        {
            return View(_contextEF.GetAll<Post>()
                .Include(posts => posts.Creater)
                .Include(posts => posts.UsingChat)
                .AsEnumerable());
        }

        public IActionResult Chats()
        {
            return View(_contextEF
                .GetAll<Chat>()
                .Include(chat => chat.Creater)
                .AsEnumerable());
        }

        public IActionResult Users()
        {
            return View(_userContextEF.GetAllUsers()
                .Include(user => user.UserFriends)
                .Include(user => user.FollowingUser)
                .Include(user => user.Posts)
                .Include(user => user.OwnerChats)
                .AsEnumerable());
        }

        public IActionResult Roles()
        {
            return View(_roleManager.Roles.AsEnumerable());
        }

        public async Task<IActionResult> ChatMassages(Guid chatId)
        {
            Chat chat = await _contextEF
                    .GetAll<Chat>()
                    .Include(chat => chat.UserMassage)
                    .Include(chat => chat.UserMassage)
                        .ThenInclude(massage => massage.Creater)
                    .FirstOrDefaultAsync(chat => chat.Id == chatId);

            if (chat != null)
            {
                return View(chat);
            }

            return RedirectToAction("Posts", "Admin");
        }
    }
}
