using Microsoft.AspNetCore.Mvc;
using Social_Media.Web.Controllers;
using Moq;
using Xunit;
using Social_Media.EntityFramework;
using Social_Media.Data.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Social_Media.Data.Models.Entities.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Social_Media.Data.Models.Entities_Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Social_Media.Tests.ControllerTests
{
    public class PostWallControllerTest
    {
        private List<Post> PostList()
        {
            var posts = new List<Post>()
            {
                new Post()
                {
                    Title = "New Post",
                    Discription = "I'm fine",
                    CreateAt = DateTime.Now,
                    Id = Guid.NewGuid(),
                    UsingChat = new Chat()
                    {
                        Name = "New Post",
                        Id = Guid.NewGuid(),
                        CreateAt = DateTime.Now,
                        UserMassage = new List<Massage>()
                    }
                },
                new Post()
                {
                    Title = "New Post2",
                    Discription = "I'm good",
                    CreateAt = DateTime.Now,
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UsingChat = new Chat()
                    {
                        Name = "New Post2",
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        CreateAt = DateTime.Now,
                        UserMassage = new List<Massage>()
                    }
                }
            };

            return posts;
        }

        [Fact]
        public void CreatePostTest()
        {
            var mock = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudPostWallController(mock.Object, moqUser.Object);
            var badController = new CrudPostWallController(mock.Object, moqUser.Object);
            badController.ModelState.AddModelError("Discription", "Required");

            Post post = new Post() 
            {
                Title = "New Post2",
                Discription = "I'm bad",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Id = Guid.NewGuid(),
            };

            var result = controller.CreatePost(post, "Danial");
            var result2 = badController.CreatePost(new Post() { }, "Danial");
            var result3 = controller.CreatePost(post,"Danial", "Posts/");


            var routeToCreatePost = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Posts", routeToCreatePost.ActionName);
            Assert.Equal("PostWall", routeToCreatePost.ControllerName);
            mock.Verify(mock => mock.CreateAsync(post));

            var routeToViewCreate = Assert.IsType<RedirectToActionResult>(result2.Result);
            Assert.Equal("CreatePost", routeToViewCreate.ActionName);
            Assert.Equal("PostWall", routeToViewCreate.ControllerName);

            var routeToViewCreateAnother = Assert.IsType<RedirectToRouteResult>(result3.Result);
            Assert.Equal("Posts/", routeToViewCreateAnother.RouteName);
        }

        [Fact]
        public void UpdatePostTest()
        {
            var mock = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudPostWallController(mock.Object, moqUser.Object);
            var badController = new CrudPostWallController(mock.Object, moqUser.Object);

            var post = new Post()
            {
                Title = "New Post2(Edit)",
                Discription = "I'm not bad",
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            };
            var result = controller.UpdatePost(post);
            var result2 = controller.UpdatePost(new Post());
            var result3 = controller.UpdatePost(post, "Posts/");

            var routeToUpdatePost = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Posts", routeToUpdatePost.ActionName);
            Assert.Equal("PostWall", routeToUpdatePost.ControllerName);
            mock.Verify(moq => moq.UpdateAsync(It.IsAny<Post>()));

            var routeToViewUpdatePost = Assert.IsType<RedirectToActionResult>(result2.Result);
            Assert.Equal("EditPost", routeToViewUpdatePost.ActionName);

            var routeToViewUpdatePostAnother = Assert.IsType<RedirectToRouteResult>(result3.Result);
            Assert.Equal("Posts/", routeToViewUpdatePostAnother.RouteName);
        }

        [Fact]
        public void RemovetPostTest()
        {
            var mock = GetMock();
            var moqUser = GetMockIdentity();
            var controller = new CrudPostWallController(mock.Object, moqUser.Object);

            var postId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = controller.RemovetPost(postId);
            var result2 = controller.RemovetPost(postId, "Posts/");

            var routeToRemovePost = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Posts", routeToRemovePost.ActionName);
            Assert.Equal("PostWall", routeToRemovePost.ControllerName);
            mock.Verify(moq => moq.RemovetAsync(It.IsAny<Post>()));

            var routeToRemovePostAnother = Assert.IsType<RedirectToRouteResult>(result2.Result);
            Assert.Equal("Posts/", routeToRemovePostAnother.RouteName);
        }

        private Mock<IRepositoryEntityFramework> GetMock()
        {
            Mock<IRepositoryEntityFramework> mock = new Mock<IRepositoryEntityFramework>();
            mock.Setup(repo => repo.GetAll<Post>()).Returns(PostList().AsQueryable());
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