using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers
{
    public class CrudMassageController : Controller
    {
        private IRepositoryEntityFramework _contextEF;
        public CrudMassageController(IRepositoryEntityFramework entityFramework)
        {
            _contextEF = entityFramework;
        }

        public async Task<IActionResult> CreateMassage(Massage massage, Guid chatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                await _contextEF.CreateAsync(massage);

                if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("ChatingRoom", "Chat", chatId);
                }
                else
                {
                    return RedirectToRoute(returnUrl);
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToAction("ChatingRoom", "Chat", chatId);
        }
    }
}
