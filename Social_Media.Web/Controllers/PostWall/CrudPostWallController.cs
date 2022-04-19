using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities.Interfaces;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [Authorize]
    public class CrudPostWallController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserManager<User> _userManager;

        public CrudPostWallController(IRepositoryEntityFramework entityFramework, UserManager<User> userManager)
        {
            _contextEF = entityFramework;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post, string createrName, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                post.Liked = 0;
                User user;

                Chat chatForPost = new Chat()
                {
                    Name = post.Title,
                    CreateAt = post.CreateAt,
                    UpdateAt = post.UpdateAt,
                    UserMassage = new List<Massage>(),

                };

                if (createrName != null)
                {
                    user = await _userManager.FindByNameAsync(createrName);
                    post.CreaterId = user.Id;
                    chatForPost.CreaterId = user.Id;
                }
                else
                {
                    return RedirectToAction("Posts", "PostWall");
                }


                post.UsingChat = chatForPost;
                await _contextEF.CreateAsync(post);

                if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("Posts", "PostWall");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction("CreatePost", "PostWall");
        }

        [HttpPost]
        public async Task<IActionResult> RemovetPost(Guid postId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Post post = await _contextEF.GetAll<Post>().FirstOrDefaultAsync(post => post.Id == postId);

                if (post != null)
                {
                    await _contextEF.RemovetAsync(post);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Posts", "PostWall");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    return BadRequest("Post not removed");
                }

            }
            ViewBag.Exeption = "Error";
            return RedirectToRoute(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(Post post, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Post postContext = await _contextEF.GetAll<Post>().FirstOrDefaultAsync(postContext => postContext.Id == post.Id);
                if (postContext != null)
                {
                    postContext.UpdateAt = DateTime.Now;
                    postContext.Discription = post.Discription;
                    postContext.Title = post.Title;
                    await _contextEF.UpdateAsync(postContext);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Posts", "PostWall");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("EditPost", "PostWall", new {postId = post.Id});
        }
    }
}
