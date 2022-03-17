using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return View(_contextEF.GetAll<Post>().AsEnumerable());
        }
        
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
