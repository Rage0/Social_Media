using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Social_Media.Data;
using Social_Media.Data.Models.Entities_Identity;
using Social_Media.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddIdentityCore<User>().AddEntityFrameworkStores<AppIdentityContext>();

            services.AddTransient<IRepositoryEntityFramework, ContextEntityFramework>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Posts",
                    pattern: "{controller}/{action}/",
                    defaults: new { controller = "PostWall", action = "Posts" }
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PostWall}/{action=Posts}/{id?}"
                    );
            });
        }
    }
}