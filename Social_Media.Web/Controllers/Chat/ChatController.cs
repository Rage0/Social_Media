using Microsoft.AspNetCore.Mvc;
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
            return View(GetChats);
        }

        public IActionResult CreateChat()
        {
            return View();
        }

        public IActionResult ChatingRoom(Guid chatId)
        {
            Chat chat = GetChats.FirstOrDefault(chat => chat.Id == chatId);
            if (chat != null)
            {
                return View(chat.UserMassage);
            }
            return RedirectToAction("Chats");
        }

        private IEnumerable<Chat> GetChats => _contextEF.GetAll<Chat>().AsEnumerable();
    }
}
