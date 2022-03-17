using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Models.Entities_Identity;

namespace Social_Media.Data
{
    public class AppIdentityContext : IdentityDbContext<User>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                   .HasMany(user => user.OwnerChats)
                   .WithOne(chat => chat.Creater);

            builder.Entity<User>()
                    .HasMany(user => user.MemberChats)
                    .WithMany(chat => chat.Members);

            builder.Entity<User>()
                   .HasMany(user => user.Posts)
                   .WithOne(post => post.Creater);

            builder.Entity<User>()
                   .HasMany(user => user.Massages)
                   .WithOne(massage => massage.Creater);

            builder.Entity<User>()
                   .HasMany(user => user.UserFriends)
                   .WithMany(user => user.FollowingUser);
                   

            base.OnModelCreating(builder);
        }
    }
}
