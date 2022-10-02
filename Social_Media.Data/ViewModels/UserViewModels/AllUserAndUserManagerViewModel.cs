using Microsoft.AspNetCore.Identity;
using Social_Media.Data.DataModels.Entities_Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Data.ViewModels.UserViewModels
{
    public class AllUserAndUserManagerViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public UserManager<User> UserManager { get; set; }
    }
}
