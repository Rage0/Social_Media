using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities.Interfaces;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.MassageViewModels;
using Social_Media.EntityFramework;
using Social_Media.Web.Model.UserModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> CreateMassageToChat(MassageAndChatIdViewModel viewModel, string createrName, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Chat chat = await _contextEF.GetAll<Chat>().FirstOrDefaultAsync(chat => chat.Id == viewModel.ChatId);
                User user = await _userManager.FindByNameAsync(createrName);
                if (chat != null)
                {
                    chat.UpdateAt = DateTime.Now;
                    viewModel.Massage.UsingChat = chat;
                }

                if (user != null)
                {
                    viewModel.Massage.CreaterId = user.Id;
                }

                await _contextEF.CreateAsync(viewModel.Massage);
            }
            if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("ChatingRoom", "Chat", new { chatId = viewModel.ChatId });
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMassageToPrivateChat(MassageAndPrivateChatIdViewModel viewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                PrivateChat privateChat = await _contextEF.GetAll<PrivateChat>()
                    .FirstOrDefaultAsync(chat => chat.Id == viewModel.PrivateChatId);

                User user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (privateChat != null)
                {
                    privateChat.UpdateAt = DateTime.Now;
                    viewModel.Massage.PrivateChat = privateChat;

                }

                if (user != null)
                {
                    viewModel.Massage.CreaterId = user.Id;
                }

                await _contextEF.CreateAsync(viewModel.Massage);

            }
            if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("PrivateChatingRoom", "PrivateChat", new
                {
                    model = new FriendNameAndUserName
                    {
                        UserName = viewModel.UserName,
                        FriendName = viewModel.FriendName,
                    }
                });
            }
            else
            {
                return Redirect(returnUrl);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateMassage(MassageAndChatIdViewModel viewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Massage massageContext = await _contextEF.GetAll<Massage>()
                    .FirstOrDefaultAsync(massageContext => massageContext.Id == viewModel.Massage.Id);
                if (massageContext != null)
                {
                    massageContext.UpdateAt = DateTime.Now;
                    massageContext.Discription = viewModel.Massage.Discription;
                    await _contextEF.UpdateAsync(massageContext);
                }
                else
                {
                    return BadRequest("Massage not updated");
                }
            }
            if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("ChatingRoom", "Chat", new { chatId = viewModel.ChatId });
            }
            else
            {
                return Redirect(returnUrl);
            };
        }

        [HttpPost]
        public async Task<IActionResult> RemovetMassage(Guid massageId, Guid chatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Massage massage = await _contextEF.GetAll<Massage>().FirstOrDefaultAsync(massage => massage.Id == massageId);
                if (massage != null)
                {
                    await _contextEF.RemovetAsync(massage);
                }
                else
                {
                    return BadRequest("Massage not removed");
                }
            }
            if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("ChatingRoom", "Chat", new { chatId = chatId });
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
    }
}
