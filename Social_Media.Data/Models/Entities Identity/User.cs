using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Models.Entities;
using Social_Media.Data.Models.Entities.Interfaces;

namespace Social_Media.Data.Models.Entities_Identity
{
    public class User: IdentityUser
    {
        public string PhotoProfileRoute { get; set; }
        public List<Chat> OwnerChats { get; set; }
        public List<Chat> MemberChats { get; set; }
        public List<Post> Posts { get; set; }
        public List<Massage> Massages { get; set; }
        public List<User> UserFriends { get; set; }
        public List<User> FollowingUser { get; set; }
    }
}
