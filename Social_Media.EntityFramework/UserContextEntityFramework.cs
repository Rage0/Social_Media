using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.Data.ViewModels.UserViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Media.EntityFramework
{
    public class UserContextEntityFramework
    {
        private ApplicationContext _context;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserContextEntityFramework(ApplicationContext applicationContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = applicationContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterViewModel viewModel)
        {
            User user = new User
            {
                UserName = viewModel.Name,
                Email = viewModel.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                User userFromManager = await _userManager.FindByNameAsync(user.UserName);
                if (userFromManager != null)
                {
                    _context.Users.Add(userFromManager);
                    await _context.SaveChangesAsync();
                    return result;
                }
            }
            return IdentityResult.Failed();
        }

        public async Task UpdateUserAsync(User userEdited)
        {
            _context.Users.Update(userEdited);
            await _context.SaveChangesAsync();
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string code, string password)
        {
            var result = await _userManager.ResetPasswordAsync(user, code, password);
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(user.UserName);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return result;
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> RemovetUserAsync(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
               var privateChatContext = _context.PrivateChats.Where(privateChat => privateChat.Members.Contains(user));

                foreach (PrivateChat privateChat in privateChatContext)
                {
                    _context.PrivateChats.Remove(privateChat);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return result;
            }
            return IdentityResult.Failed();
        }
    }
}
