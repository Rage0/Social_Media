using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Social_Media.Data.DataModels.Entities;


namespace Social_Media.Data.DataModels.Entities_Identity
{
    public class User: IdentityUser
    {
        public string PhotoProfileRoute { get; set; }
        [NotMapped]
        public List<Chat> OwnerChats { get; set; }
        public List<PrivateChat> PrivateChats { get; set; }
        [NotMapped]
        public List<Post> Posts { get; set; }
        [NotMapped]
        public List<Massage> Massages { get; set; }
        public List<User> UserFriends { get; set; }
        public List<User> FollowingUser { get; set; }
    }
}
