using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Module6_7_Palmer.Models;

namespace Module6_7_Palmer
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
            services.AddControllersWithViews();
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OrderContext")));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:  "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "admin",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Home}/{action=Index}/manage/{model?}/{type?}/{val?}"
                );
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "Price",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "custom1",
                    pattern: "{controller=Home}/{action=Index}/product/{prodName?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapAreaControllerRoute(
            //      name: "admin1",
            //      areaName: "Admin",
            //      pattern: "Admin/{controller=Home}/{action=Index}/manage"
            //    );
            //});

        }
    }
}
