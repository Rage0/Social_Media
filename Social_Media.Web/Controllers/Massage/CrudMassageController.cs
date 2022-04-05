using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Models.MassageViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class CrudMassageController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserManager<User> _userManager;
        public CrudMassageController(IRepositoryEntityFramework entityFramework, UserManager<User> userMangaer)
        {
            _contextEF = entityFramework;
            _userManager = userMangaer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMassage(MassageAndChatIdViewModel viewModel, string createrName, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Chat chat = _contextEF.GetAll<Chat>().FirstOrDefault(chat => chat.Id == viewModel.ChatId);
                User user = await _userManager.FindByNameAsync(createrName);
                if (chat != null)
                {
                    viewModel.Massage.UsingChat = chat;
                }

                if (user != null)
                {
                    viewModel.Massage.CreaterId = user.Id;
                }

                await _contextEF.CreateAsync(viewModel.Massage);

                if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("ChatingRoom", "Chat", new { chatId = viewModel.ChatId});
                }
                else
                {
                    return RedirectToRoute(returnUrl);
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToAction("ChatingRoom", "Chat", new { chatId = viewModel.ChatId });
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateMassage(MassageAndChatIdViewModel viewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Massage massageContext = _contextEF.GetAll<Massage>().FirstOrDefault(massageContext => massageContext.Id == viewModel.Massage.Id);
                if (massageContext != null)
                {
                    massageContext.UpdateAt = DateTime.Now;
                    massageContext.Discription = viewModel.Massage.Discription;
                    await _contextEF.UpdateAsync(massageContext);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("ChatingRoom", "Chat", new { chatId = viewModel.ChatId });
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
                else
                {
                    return BadRequest("Massage not updated");
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToAction("ChatingRoom", "Chat", new { chatId = viewModel.ChatId });
        }

        [HttpPost]
        public async Task<IActionResult> RemovetMassage(Guid massageId, Guid chatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Massage massage = _contextEF.GetAll<Massage>().FirstOrDefault(massage => massage.Id == massageId);
                if (massage != null)
                {
                    await _contextEF.RemovetAsync(massage);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("ChatingRoom", "Chat", new { chatId = chatId });
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
                else
                {
                    return BadRequest("Massage not removed");
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToAction("ChatingRoom", "Chat", new { chatId = chatId });
        }
    }
}
