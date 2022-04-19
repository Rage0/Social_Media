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

        public UserContextEntityFramework(ApplicationContext applicationContext, UserManager<User> userManager)
        {
            _context = applicationContext;
            _userManager = userManager;
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

        public async Task<IdentityResult> UpdateUserAsync(User userEdited)
        {
            _context.Users.Update(userEdited);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
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
