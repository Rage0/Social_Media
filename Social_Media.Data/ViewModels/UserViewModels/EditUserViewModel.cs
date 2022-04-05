using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities_Identity;
using System.Collections.Generic;

namespace Social_Media.Web.Models.UserViewModels
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
