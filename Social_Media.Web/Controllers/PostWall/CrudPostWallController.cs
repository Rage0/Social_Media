using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class CrudPostWallController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        public CrudPostWallController(IRepositoryEntityFramework entityFramework)
        {
            _contextEF = entityFramework;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                post.Liked = 0;

                // Add a Creator

                Chat chatForPost = new Chat()
                {
                    Name = post.Title,
                    CreateAt = post.CreateAt,
                    UpdateAt = post.UpdateAt,
                    UserMassage = new List<Massage>(),
                };

                post.UsingChat = chatForPost;
                await _contextEF.CreateAsync(post);

                if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("Posts", "PostWall");
                }
                else
                {
                    return RedirectToRoute(returnUrl);
                }
            }
            return RedirectToAction("CreatePost", "PostWall");
        }

        [HttpPost]
        public async Task<IActionResult> RemovetPost(Guid postId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Post post = _contextEF.GetAll<Post>().FirstOrDefault(post => post.Id == postId);

                if (post != null)
                {
                    await _contextEF.RemovetAsync(post);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Posts", "PostWall");
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
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
                Post postContext = _contextEF.GetAll<Post>().FirstOrDefault(postContext => postContext.Id == post.Id);
                if (postContext != null)
                {
                    post.UpdateAt = DateTime.Now;
                    await _contextEF.UpdateAsync(post);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Posts", "PostWall");
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
            }
            return RedirectToAction("EditPost", "Post");
        }
    }
}
