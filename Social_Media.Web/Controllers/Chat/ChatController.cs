using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Models.Entities;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Social_Media.Web.Controllers
{
    public class ChatController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        public ChatController(IRepositoryEntityFramework entityFramework)
        {
            _contextEF = entityFramework;
        }

        public IActionResult Chats()
        {
            return View(_contextEF.GetAll<Chat>().AsEnumerable());
        }

        public IActionResult CreateChat()
        {
            return View();
        }

        public IActionResult ChatingRoom(Guid chatId)
        {
            Chat chat = _contextEF.GetAll<Chat>().Include(chat => chat.UserMassage).FirstOrDefault(chat => chat.Id == chatId);
            if (chat != null)
            {
                return View(chat);
            }
            return RedirectToAction("Chats");
        }
    }
}
