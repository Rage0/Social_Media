using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
    public class PrivateChatController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserContextEntityFramework _userContextEF;
        public PrivateChatController(IRepositoryEntityFramework entityFramework, UserContextEntityFramework userEntityFramework)
        {
            _contextEF = entityFramework;
            _userContextEF = userEntityFramework;
        }

        public async Task<IActionResult> PrivateChatingRoom(FriendNameAndUserName model)
        {
            var userContext = _userContextEF.GetAllUsers()
                .Include(user => user.PrivateChats);

            User user = await userContext
                .FirstOrDefaultAsync(user => user.UserName == model.UserName);

            User friendUser = await userContext
                .FirstOrDefaultAsync(user => user.UserName == model.FriendName);


            foreach (PrivateChat privateChat in user.PrivateChats)
            {
                var id = privateChat.Id;
                foreach (PrivateChat friendPrivateChat in friendUser.PrivateChats)
                {
                    var chatId = friendPrivateChat.Id;

                    if (id == chatId)
                    {
                       var privateChatContext = await _contextEF.GetAll<PrivateChat>()
                            .Include(privateChat => privateChat.Massages)
                            .FirstOrDefaultAsync(privateChat => privateChat.Id == id);

                        return View(privateChatContext);
                    }
                }
            }

            return RedirectToAction("MyFriends", "Account", new {userName = user.UserName});
        }

        public async Task<IActionResult> MyPrivateChat(string userName)
        {
            User user = await _userContextEF.GetAllUsers()
                .Include(user => user.PrivateChats)
                    .ThenInclude(privateChat => privateChat.Members)
                .Include(user => user.Massages)
                .FirstOrDefaultAsync(user => user.UserName == userName);

            if (user != null)
            {
                return View(user.PrivateChats.AsEnumerable());
            }
            else
            {
                return RedirectToAction("Posts", "PostWall");
            }
        }
    }
}
