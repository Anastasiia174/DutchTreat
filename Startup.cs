using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;                
            })
                .AddEntityFrameworkStores<DutchContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer();
                

            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("DutchConnectionString"));
            });

            services.AddTransient<DutchSeeder>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IDutchRepository, DutchRepository>();

            services.AddTransient<IMailService, NullMailService>();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
            }
            
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseNodeModules();

            app.UseAuthentication(); 
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" });

                endpoints.MapRazorPages();
            });
        }
    }
}
