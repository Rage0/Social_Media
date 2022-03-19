using Microsoft.AspNetCore.Mvc;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;
using Social_Media.EntityFramework;
using System;
using System.Linq;
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

        [HttpPost]
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
        
        [HttpPost]
        public async Task<IActionResult> UpdateMassage(Massage massage, Guid chatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Massage massageContext = _contextEF.GetAll<Massage>().FirstOrDefault(massageContext => massageContext.Id == massage.Id);
                if (massageContext != null)
                {
                    massageContext.UpdateAt = DateTime.Now;
                    massageContext.Discription = massage.Discription;
                    await _contextEF.UpdateAsync(massageContext);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("ChatingRoom", "Chat", chatId);
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
                else
                {
                    return BadRequest("Massage not updated");
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToAction("ChatingRoom", "Chat", chatId);
        }

        [HttpPost]
        public async Task<IActionResult> RemovetMassage(Guid massageId, Guid chatId, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                Massage massage = _contextEF.GetAll<Massage>().FirstOrDefault(massage => massage.Id == massageId);
                if (massage != null)
                {
                    await _contextEF.RemovetAsync(massage);

                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("ChatingRoom", "Chat", chatId);
                    }
                    else
                    {
                        return RedirectToRoute(returnUrl);
                    }
                }
                else
                {
                    return BadRequest("Massage not removed");
                }
            }
            ViewBag.Exeption = "Error";
            return RedirectToAction("ChatingRoom", "Chat", chatId);
        }
    }
}
