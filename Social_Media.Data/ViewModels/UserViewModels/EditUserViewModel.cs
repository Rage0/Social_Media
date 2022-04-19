using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities_Identity;
using System.Collections.Generic;

namespace Social_Media.Data.ViewModels.UserViewModels
{
    public class EditUserViewModel
    {
        public string Name { get; set; }
        public string PhotoProfileRoute { get; set; }
        public string Id { get; set; }
        public Post[] DeletePosts { get; set; }
        public User[] UnfollowUsers { get; set; }
    }
}
