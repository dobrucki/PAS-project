using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PAS_project.Controllers;
using PAS_project.Models;
using PAS_project.Models.Entities;
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
            services.AddMvc();
            
            // DI
            services.AddSingleton<IDataRepository<Movie>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<Movie>>(x, 10000));
            services.AddSingleton<MovieManager, MovieManager>();
            services.AddSingleton<IDataRepository<Seance>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<Seance>>(x, 20000));
            services.AddSingleton<SeanceManager, SeanceManager>();
            services.AddSingleton<IDataRepository<User>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<User>>(x, 30000));
            services.AddSingleton<UserManager, UserManager>();
            services.AddSingleton<IDataRepository<CinemaEvent>>(
                x => ActivatorUtilities.CreateInstance<MockDataRepository<CinemaEvent>>(x, 40000));
            services.AddSingleton<CinemaEventManager, CinemaEventManager>();
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );
        }
    }
}