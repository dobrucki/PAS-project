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
            // Dependency Injection
            services.AddSingleton<IDataRepository<ICinemaEvent>, CinemaEventRepository>();
            services.AddSingleton<IDataRepository<Movie>, MovieRepository>();
            services.AddSingleton<MovieManager, MovieManager>();
            services.AddSingleton<IDataRepository<Seance>, SeanceRepository>();
            services.AddSingleton<SeanceManager, SeanceManager>();
            services.AddSingleton<IDataRepository<Hall>, HallRepository>();
            services.AddSingleton<HallManager, HallManager>();
            services.AddSingleton<IDataRepository<User>, UserRepository>();
            services.AddSingleton<UserManager, UserManager>();
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
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
                }
            );
        }
    }
}