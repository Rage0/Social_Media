using Social_Media.Data.DataModels.Entities_Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Data.ViewModels.UserViewModels
{
    public class UserAndAllUsersViewModel
    {
        public User User { get; set; }
        public ICollection<User> UsersFromContext { get; set; }
    }
}
