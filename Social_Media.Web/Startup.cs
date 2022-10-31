using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Social_Media.Data;
using Social_Media.Data.DataModels.Entities_Identity;
using Social_Media.EntityFramework;

namespace Social_Media.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                assembly => assembly.MigrationsAssembly("Social_Media.Migrations"));
            });

            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("IdentityConnection"),
                assembly => assembly.MigrationsAssembly("Social_Media.Migrations"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IRepositoryEntityFramework, ContextEntityFramework>();
            services.AddTransient<UserContextEntityFramework>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();   
            app.UseAuthorization();

            app.UseStaticFiles();

            AppIdentityContext.CreateBaseAccountAsync(app.ApplicationServices);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute
                (
                name: "PrivateChatingRoom",
                pattern: "ChatingRoom",
                defaults: new { Controller = "PrivateChat", Action = "PrivateChatingRoom" }
                );

                endpoints.MapControllerRoute
                (
                name: "PrivateChat",
                pattern: "{UserName}/Chats",
                defaults: new { Controller = "PrivateChat", Action = "MyPrivateChat" }
                );

                endpoints.MapControllerRoute
                (
                name: "Chats",
                pattern: "Chats",
                defaults: new { Controller = "Chat", Action = "Chats" }
                );

                endpoints.MapControllerRoute
                (
                name: "AdminChats",
                pattern: "Admin/Chat",
                defaults: new { Controller = "Admin", Action = "Chats" }
                );

                endpoints.MapControllerRoute
                (
                name: "AdminChats",
                pattern: "Admin/Post",
                defaults: new { Controller = "Admin", Action = "Posts" }
                );

                endpoints.MapControllerRoute
                (
                name: "AdminChats",
                pattern: "Admin/User",
                defaults: new { Controller = "Admin", Action = "Users" }
                );

                endpoints.MapControllerRoute
                (
                name: "AdminChats",
                pattern: "Admin/Role",
                defaults: new { Controller = "Admin", Action = "Roles" }
                );

                endpoints.MapControllerRoute
                (
                name: "ChatingRoom",
                pattern: "Chat/{Action}",
                defaults: new { Controller = "Chat", Action = "ChatingRoom" }
                );

                endpoints.MapControllerRoute
                (
                name: "default",
                pattern: "{controller=PostWall}/{action=Posts}/{id?}"
                );
            });
        }
    }
}
