using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class CrudChatController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserManager<User> _userManager;
        public CrudChatController(IRepositoryEntityFramework entityFramework, UserManager<User> userMangaer)
        {
            _contextEF = entityFramework;
            _userManager = userMangaer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(Chat chat, string createrName, string returnUrl = "Chats/")
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(createrName);

                chat.CreateAt = DateTime.Now;
                chat.UpdateAt = DateTime.Now;
                chat.Members = new List<User>();
                chat.UserMassage = new List<Massage>();
                if (user != null)
                {
                    chat.CreaterId = user.Id;
                }
                

                await _contextEF.CreateAsync(chat);
                return RedirectToAction(returnUrl);
            }
            return RedirectToAction("CreateChat", "Chat");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChat(Chat chat, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Chat chatContext = _contextEF.GetAll<Chat>().FirstOrDefault(chatContext => chatContext.Id == chat.Id);
                if (chatContext != null)
                {
                    chatContext.UpdateAt = DateTime.Now;
                    chatContext.Name = chat.Name;

                    await _contextEF.UpdateAsync(chatContext);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Chats", "Chat");
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }

            }
            return RedirectToAction("EditChat", "Chat", new {chatId = chat.Id});
        }

        [HttpPost]
        public async Task<IActionResult> RemovetChat(Guid chatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Chat chat = _contextEF.GetAll<Chat>().FirstOrDefault(chat => chat.Id == chatId);

                if (chat != null)
                {
                    await _contextEF.RemovetAsync(chat);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Chats", "Chat");
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
                else
                {
                    return BadRequest("Chat not removed");
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToRoute(returnUrl);
        }
    }
}
