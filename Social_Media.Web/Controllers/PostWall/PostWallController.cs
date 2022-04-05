using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities_Identity;
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
        public PostWallController(IRepositoryEntityFramework entityFramework)
        {
            _contextEF = entityFramework;
        }

        public IActionResult Posts()
        {
            return View(_contextEF.GetAll<Post>()
                .Include(post => post.UsingChat)
                .Include(post => post.Creater)
                .AsEnumerable());
        }
        
        public IActionResult CreatePost()
        {
            return View();
        }

        public async Task<IActionResult> EditPost(Guid postId)
        {
            Post post = await _contextEF.GetAll<Post>().FirstOrDefaultAsync(post => post.Id == postId);
            if (post != null)
            {
                return View(post);
            }
            else
            {
                return RedirectToAction("Posts");
            }
        }
    }
}
