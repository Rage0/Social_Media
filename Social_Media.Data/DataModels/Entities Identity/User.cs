using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;

namespace Social_Media.Data.Models.Entities_Identity
{
    public class User: IdentityUser
    {
        public string PhotoProfileRoute { get; set; }
        [NotMapped]
        public List<Chat> OwnerChats { get; set; }
        [NotMapped]
        public List<Chat> MemberChats { get; set; }
        [NotMapped]
        public List<Post> Posts { get; set; }
        [NotMapped]
        public List<Massage> Massages { get; set; }
        public List<User> UserFriends { get; set; }
        public List<User> FollowingUser { get; set; }
    }
}
