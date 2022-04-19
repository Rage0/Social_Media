using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
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
        public ChatController(IRepositoryEntityFramework entityFramework)
        {
            _contextEF = entityFramework;
        }

        public IActionResult Chats()
        {
            return View(_contextEF.GetAll<Chat>().AsEnumerable().Where(chat => chat.PostId == null));
        }

        public IActionResult CreateChat()
        {
            return View();
        }

        public async Task<IActionResult> ChatingRoom(Guid chatId)
        {
            Chat chat = await _contextEF.GetAll<Chat>()
                .Include(chat => chat.UserMassage)
                .ThenInclude(massage => massage.Creater)
                .FirstOrDefaultAsync(chat => chat.Id == chatId);
            if (chat != null)
            {
                return View(chat);
            }
            return RedirectToAction("Chats");
        }
    }
}
