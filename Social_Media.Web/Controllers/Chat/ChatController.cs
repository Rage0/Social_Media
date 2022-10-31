using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserManager<User> _userManager;
        public ChatController(IRepositoryEntityFramework entityFramework, UserManager<User> userManager)
        {
            _contextEF = entityFramework;
            _userManager = userManager;
        }

        public IActionResult Chats()
        {
            return View(_contextEF.GetAll<Chat>().AsEnumerable().Where(chat => chat.PostId == null));
        }

        public async Task<IActionResult> UserChats(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                return View(_contextEF.GetAll<Chat>()
                    .AsEnumerable()
                    .Where(chat => chat.CreaterId == user.Id && chat.PostId == null));
            }
            else
            {
                return RedirectToAction("Posts", "PostWall");
            }
            
        }

        public IActionResult CreateChat()
        {
            return View(new Chat());
        }

        public async Task<IActionResult> ChatingRoom(Guid chatId)
        {
            Chat chat = await _contextEF.GetAll<Chat>()
                .Include(chat => chat.UserMassage)
                .ThenInclude(massage => massage.Creater)
                .FirstOrDefaultAsync(chat => chat.Id == chatId);

            User user = await _userManager.Users.
                FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);

            if (chat != null)
            {
                ViewBag.IdUser = user.Id;
                return View(chat);
            }
            return RedirectToAction("Chats");
        }
    }
}
