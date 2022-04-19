using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Infrastructure;
using Social_Media.Web.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
    public class CrudPrivateChatController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserContextEntityFramework _userContextEF;
        public CrudPrivateChatController(IRepositoryEntityFramework entityFramework, UserContextEntityFramework userContext)
        {
            _contextEF = entityFramework;
            _userContextEF = userContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrivateChat(FriendNameAndUserName model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                User user = await _userContextEF.GetAllUsers()
                    .Include(user => user.PrivateChats)
                    .FirstOrDefaultAsync(user => user.UserName == model.UserName);

                User friendUser = await _userContextEF.GetAllUsers()
                    .Include(user => user.PrivateChats)
                    .FirstOrDefaultAsync(user => user.UserName == model.FriendName);

                bool existChat = user.PrivateChats.HasSamePrivateChat(friendUser.PrivateChats);

                if (user != null && friendUser != null && !existChat)
                {
                    PrivateChat privateChat = new PrivateChat
                    {
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        Members = new List<User>() { user, friendUser}
                    };

                    await _contextEF.CreateAsync(privateChat);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("MyPrivateChat", "PrivateChat", new {userName = user.UserName});
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("Posts", "PostWall");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrivateChat(PrivateChat privateChat, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                PrivateChat privateChatContext = await _contextEF.GetAll<PrivateChat>()
                                .Include(privateChatContext => privateChatContext.Members)
                                .Include(privateChatContext => privateChatContext.Massages)
                                .FirstOrDefaultAsync(privateChatContext => privateChatContext.Id == privateChat.Id);

                if (privateChatContext != null)
                {
                    privateChatContext.UpdateAt = DateTime.Now;
                    privateChatContext.Members = privateChat.Members;
                    privateChatContext.Massages = privateChatContext.Massages;

                    await _contextEF.UpdateAsync(privateChatContext);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Chats", "Chat");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("Posts", "PostWall");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePrivateChat(Guid privateChatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                PrivateChat privateChat = await _contextEF.GetAll<PrivateChat>()
                        .FirstOrDefaultAsync(privateChat => privateChat.Id == privateChatId);

                if (privateChat != null)
                {
                    await _contextEF.RemovetAsync(privateChat);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Chats", "Chat");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("Posts", "PostWall");
        }
    }
}
