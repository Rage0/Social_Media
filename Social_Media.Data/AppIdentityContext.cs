using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Social_Media.Data.DataModels.Entities_Identity;

namespace Social_Media.Data
{
    public class AppIdentityContext : IdentityDbContext<User>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public static async Task CreateBaseAccountAsync(IServiceProvider service)
        {
            AppIdentityContext identityContext = service
                .GetRequiredService<AppIdentityContext>();

            RoleManager<IdentityRole> role = service
                .GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<User> userManager = service
                .GetRequiredService<UserManager<User>>();

            ApplicationContext context = service
                .GetRequiredService<ApplicationContext>();

            if (!identityContext.Roles.Any())
            {
                await role.CreateAsync(new IdentityRole("Admin"));
                await role.CreateAsync(new IdentityRole("User"));
            }

            if (!identityContext.Users.Any())
            {
                string password = "NeverGiveYourUp";
                User user = new User
                {
                    UserName = "AdminUser",
                    Email = "admin@email.com"
                };

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!context.Users.Any())
            {
                User admin = await userManager.FindByNameAsync("AdminUser");
                if (admin != null)
                {
                    context.Users.Add(admin);
                    context.SaveChanges();
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
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
