using Microsoft.AspNetCore.Mvc;

namespace Social_Media.Web.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult EditRole()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }
    }
}
