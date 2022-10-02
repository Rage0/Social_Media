using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.ViewModels.UserViewModels.RoleViewModel;
using System.Threading.Tasks;

namespace Social_Media.Web.Controllers.Role
{
    public class CrudRoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public CrudRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName, string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(roleName) || !string.IsNullOrWhiteSpace(roleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Post", "PostWall");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("CreateRole", "Role");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(EditRoleViewModel model, string returnUrl = "")
        {
            IdentityRole role = await _roleManager
                .Roles
                .FirstOrDefaultAsync(roleContext => roleContext.Name == model.roleName);

            if (role != null)
            {
                if (!string.IsNullOrEmpty(model.roleNameEdited) || !string.IsNullOrWhiteSpace(model.roleNameEdited))
                {
                    role.Name = model.roleNameEdited;
                    IdentityResult result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Post", "PostWall");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                }
            }
            return RedirectToAction("EditRole", "Role");
        }

        [HttpPost]
        public async Task<IActionResult> RemovetRole(string id, string returnUrl = "")
        {
            IdentityRole role = await _roleManager
                                .Roles
                                .FirstOrDefaultAsync(roleContext => roleContext.Id == id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl) || string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Post", "PostWall");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            return RedirectToAction("Post", "PostWall");
        }
    }
}
