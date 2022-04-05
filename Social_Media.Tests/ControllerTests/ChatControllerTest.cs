using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Social_Media.Tests.ControllerTests
{
    public class ChatControllerTest
    {
        private List<Chat> ChatList()
        {
            return new List<Chat>() { new Chat
            {
                Name = "New Chat",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserMassage = new List<Massage>()
                {
                    new Massage
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Discription = "I trusted you",
                    }
                }
            }};
        }

        [Fact]
        public void ViewModelChatsTest()
        {
            var moq = GetMock();
            var controller = new ChatController(moq.Object);

            var model = controller.Chats();

            var result = (model as ViewResult).ViewData?.Model as IEnumerable<Chat>;
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateChatTest()
        {
            var moq = GetMock();
            var moqUser = GetMockIdentity();

            var controller = new CrudChatController(moq.Object, moqUser.Object);
            var badController = new CrudChatController(moq.Object, moqUser.Object);
            badController.ModelState.AddModelError("Name", "Required");

            var chat = new Chat
            {
                Name = "New Chat 2",
                Id = Guid.NewGuid(),
            };
            var result = controller.CreateChat(chat, "Danial");
            var result2 = badController.CreateChat(new Chat(), "Danial");

            var routeToCreateChat = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Chats/", routeToCreateChat.ActionName);
            moq.Verify(mock => mock.CreateAsync(chat));

            var routeToViewCreate = Assert.IsType<RedirectToActionResult>(result2.Result);
            Assert.Equal("CreateChat", routeToViewCreate.ActionName);
            Assert.Equal("Chat", routeToViewCreate.ControllerName);
        }

        [Fact]
        public void UpdateChatTest()
        {
            var moq = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudChatController(moq.Object, moqUser.Object);

            var chat = new Chat
            {
                Name = "New Chat(Edit)",
                UpdateAt = DateTime.Now,
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111")
            };
            var result = controller.UpdateChat(chat);
            var result2 = controller.UpdateChat(new Chat());
            var result3 = controller.UpdateChat(chat, "Posts/");

            var routeToUpdateChat = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Chats", routeToUpdateChat.ActionName);
            Assert.Equal("Chat", routeToUpdateChat.ControllerName);
            moq.Verify(repo => repo.UpdateAsync(It.IsAny<Chat>()));

            var routeToViewUpdateChat = Assert.IsType<RedirectToActionResult>(result2.Result);
            Assert.Equal("EditChat", routeToViewUpdateChat.ActionName);
            Assert.Equal("Chat", routeToViewUpdateChat.ControllerName);

            var routeToViewUpdateChatAnother = Assert.IsType<RedirectToRouteResult>(result3.Result);
            Assert.Equal("Posts/", routeToViewUpdateChatAnother.RouteName);
        }

        [Fact]
        public void RemovetChatTest()
        {
            var moq = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudChatController(moq.Object, moqUser.Object);

            var chatId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = controller.RemovetChat(chatId);
            var result2 = controller.RemovetChat(chatId, "Posts/");

            var routeToRemovetChat = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Chats", routeToRemovetChat.ActionName);
            Assert.Equal("Chat", routeToRemovetChat.ControllerName);
            moq.Verify(repo => repo.RemovetAsync(It.IsAny<Chat>()));

            var routeToViewRemovetChat = Assert.IsType<RedirectToRouteResult>(result2.Result);
            Assert.Equal("Posts/", routeToViewRemovetChat.RouteName);

        }

        [Fact]
        public void ViewModelChatingRoomTest()
        {
            var moq = GetMock();
            var controller = new ChatController(moq.Object);

            var result2 = controller.ChatingRoom(Guid.NewGuid());

            var routeToViewChatingRoom = Assert.IsType<RedirectToActionResult>(result2);
            Assert.Equal("Chats", routeToViewChatingRoom.ActionName);
        }

        private Mock<IRepositoryEntityFramework> GetMock()
        {
            var moq = new Mock<IRepositoryEntityFramework>();
            moq.Setup(repo => repo.GetAll<Chat>()).Returns(ChatList().AsQueryable);
            return moq;
        }
        private Mock<UserManager<User>> GetMockIdentity()
        {
            var moqUser = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(moqUser.Object, null, null, null, null, null, null, null, null);
            return mgr;
        }
    }
}
