using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    [AllowAnonymous]
    public class PostWallController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        private UserContextEntityFramework _userContextEF;
        public PostWallController(IRepositoryEntityFramework entityFramework, UserContextEntityFramework userContext)
        {
            _contextEF = entityFramework;
            _userContextEF = userContext;
        }

        public IActionResult Posts()
        {
            return View(_contextEF.GetAll<Post>()
                .Include(post => post.UsingChat)
                .Include(post => post.Creater)
                .AsEnumerable());
        }

        [Authorize]
        public IActionResult CreatePost()
        {
            return View(new Post());
        }

        [Authorize]
        public async Task<IActionResult> EditPost(Guid postId, string returnUrl = "")
        {
            Post post = await _contextEF.GetAll<Post>().FirstOrDefaultAsync(post => post.Id == postId);
            if (post != null)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(post);
            }
            else
            {
                return RedirectToAction("Posts");
            }
        }
    }
}
