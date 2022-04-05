using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.EntityFramework;
using Social_Media.Web.Controllers;
using Social_Media.Web.Models.MassageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Social_Media.Tests.ControllerTests
{
    public class MassageControllerTest
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
            }};
        }
        private List<Massage> MassageList()
        {
            return new List<Massage>()
            {
                new Massage
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Discription = "I trusted you",
                }
            };
        }

        [Fact]
        public void CreateMassageTest()
        {
            var mock = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudMassageController(mock.Object, moqUser.Object);

            var viewModel = new MassageAndChatIdViewModel
            {
                Massage = new Massage
                {
                    Discription = "Why!?",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Id = Guid.Parse("11111110-1110-1110-1110-111111111110"),
                    UsingChat = ChatList().First()
                },
                ChatId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            };
            var result = controller.CreateMassage(viewModel, "Danial");

            var routeToCreateMassage = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("ChatingRoom", routeToCreateMassage.ActionName);
            Assert.Equal("Chat", routeToCreateMassage.ControllerName);
            mock.Verify(moq => moq.CreateAsync(viewModel.Massage));

        }

        [Fact]
        public void UpdateMassageTest()
        {
            var mock = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudMassageController(mock.Object, moqUser.Object);

            var viewModel = new MassageAndChatIdViewModel
            {
                Massage = new Massage
                {
                    Discription = "I'm fine",
                    UpdateAt = DateTime.Now,
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                },
                ChatId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            };
            var result = controller.UpdateMassage(viewModel);
            var result2 = controller.UpdateMassage(viewModel, "Posts/");

            var routeToUpdateMassage = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("ChatingRoom", routeToUpdateMassage.ActionName);
            Assert.Equal("Chat", routeToUpdateMassage.ControllerName);
            mock.Verify(repo => repo.UpdateAsync(It.IsAny<Massage>()));

            var routeToUpdateMassageAnother = Assert.IsType<RedirectToRouteResult>(result2.Result);
            Assert.Equal("Posts/", routeToUpdateMassageAnother.RouteName);
        }

        [Fact]
        public void RemovetMassageTest()
        {
            var mock = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudMassageController(mock.Object, moqUser.Object);

            var massageId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var chatId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = controller.RemovetMassage(massageId, chatId);
            var result2 = controller.RemovetMassage(massageId, chatId,"Posts/");

            var routeToRemoveMassage = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("ChatingRoom", routeToRemoveMassage.ActionName);
            Assert.Equal("Chat", routeToRemoveMassage.ControllerName);
            mock.Verify(repo => repo.RemovetAsync(It.IsAny<Massage>()));

            var routeToRemoveMassageAnother = Assert.IsType<RedirectToRouteResult>(result2.Result);
            Assert.Equal("Posts/", routeToRemoveMassageAnother.RouteName);
        }

        private Mock<IRepositoryEntityFramework> GetMock()
        {
            Mock<IRepositoryEntityFramework> mock = new Mock<IRepositoryEntityFramework>();
            mock.Setup(repo => repo.GetAll<Massage>()).Returns(MassageList().AsQueryable());
            return mock;
        }

        private Mock<UserManager<User>> GetMockIdentity()
        {
            var moqUser = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(moqUser.Object, null, null, null, null, null, null, null, null);
            return mgr;
        }
    }
}
