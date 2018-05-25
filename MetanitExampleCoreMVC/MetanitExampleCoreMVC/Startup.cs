using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetanitExampleCoreMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MetanitExampleCoreMVC.Services;
using Microsoft.AspNetCore.Mvc;
using MetanitExampleCoreMVC.Util;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace MetanitExampleCoreMVC
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MobileContext>(options => options.UseSqlServer(connection));
            services.AddTransient<ITimeService, SimpleTimeService>();
            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddTransient<IRepository, PhoneRepository>();
            services.Configure<MvcViewOptions>(options =>
            {
                options.ViewEngines.Clear();
                options.ViewEngines.Add(new CustomViewEngine());
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseMvcWithDefaultRoute();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}");
                routes.MapRoute("api/get", async context =>
                 {
                     context.Response.ContentType = "text/html;utf-8";
                     await context.Response.WriteAsync("для обработки использован маршрут api / get");
                 });
            });
        }
    }
}
