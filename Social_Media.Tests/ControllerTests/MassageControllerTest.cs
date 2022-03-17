using Microsoft.AspNetCore.Mvc;
using Moq;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.EntityFramework;
using Social_Media.Web.Controllers;
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
            var controller = new CrudMassageController(mock.Object);

            var massage = new Massage
            {
                Discription = "Why!?",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id = Guid.Parse("11111110-1110-1110-1110-111111111110"),
                UsingChat = ChatList().First()
            };
            var result = controller.CreateMassage(massage, ChatList().First().Id);

            var routeToCreateMassage = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ChatingRoom", routeToCreateMassage.ActionName);
            Assert.Equal("Chat", routeToCreateMassage.ControllerName);
            mock.Verify(moq => moq.CreateAsync(massage));

        }

        [Fact]
        public void RemovetMassageTest()
        {
            var mock = GetMock();
            var controller = new CrudMassageController(mock.Object);

            var massageId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var chatId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = controller.RemovetMassage(massageId, chatId);
            var result2 = controller.RemovetMassage(massageId, chatId,"Posts/");

            var routeToRemoveMassage = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ChatingRoom", routeToRemoveMassage.ActionName);
            Assert.Equal("Chat", routeToRemoveMassage.ControllerName);
            mock.Verify(repo => repo.RemovetAsync(It.IsAny<Massage>()));

            var routeToRemoveMassageAnother = Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("Posts/", routeToRemoveMassageAnother.RouteName);
        }

        private Mock<IRepositoryEntityFramework> GetMock()
        {
            Mock<IRepositoryEntityFramework> mock = new Mock<IRepositoryEntityFramework>();
            mock.Setup(repo => repo.GetAll<Massage>()).Returns(MassageList().AsQueryable());
            return mock;
        }
    }
}
