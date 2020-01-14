using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PAS_project.Controllers;
using PAS_project.Models;
using PAS_project.Models.Entities;
using PAS_project.Models.Identity;
using PAS_project.Models.Managers;
using PAS_project.Models.Repositories;

namespace PAS_project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/SignIn");
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            // DI
            services.AddSingleton<IDataRepository<Movie>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<Movie>>(x, 10000));
            services.AddSingleton<MovieManager>();
            services.AddSingleton<IDataRepository<Seance>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<Seance>>(x, 20000));
            services.AddSingleton<SeanceManager>();
            services.AddSingleton<IDataRepository<ApplicationUser>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<ApplicationUser>>(x, 30000));
            services.AddSingleton<UserManager>();
            services.AddSingleton<IDataContext, DataContext>(x => RandomDataFactory.GenerateRandomData());
            services.AddSingleton<IDataRepository<CinemaEvent>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<CinemaEvent>>(x, 40000));
            services.AddSingleton<CinemaEventManager>();
            services.AddSingleton<IDataRepository<CinemaHall>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<CinemaHall>>(x, 50000));
            services.AddSingleton<CinemaHallManager>();
            services.AddSingleton<IDataRepository<ApplicationRole>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<ApplicationRole>>(x, 60000));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("Home/Error");
            }

            
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvc(routes =>
                {
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );

//            app.UseRouting();
//
//            app.UseEndpoints(endpoints =>
//                {
//                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//                }
//            );
        }
    }
}