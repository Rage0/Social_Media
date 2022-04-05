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
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(user => user.UserFriends)
                .WithMany(user => user.FollowingUser)
                .UsingEntity(entity => entity.ToTable("FriendsAndFollowingUser"));

            base.OnModelCreating(builder);
        }
    }
}
