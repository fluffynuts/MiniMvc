using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeanutButter.Utils;
using WebOptimizer;

namespace MiniMvc
{
    public static class BundleConfig
    {
        public static string AB = "/bundles/ab.js";

        public static void RegisterBundles(IAssetPipeline pipeline)
        {
            pipeline.AddJavaScriptBundle(AB, "js/a.js", "js/b.js");
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // required to manually map routes
            // services.AddMvc(o => o.EnableEndpointRouting = false);
            services.AddControllersWithViews(options =>
            {
            });
            services.AddRazorPages();
            services.AddWebOptimizer(BundleConfig.RegisterBundles);
            services.AddResponseCompression(opts =>
                // https://github.com/ligershark/WebOptimizer#https-compression-considerations
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "text/javascript" }
                        .ToArray()
                )
            );
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebOptimizer();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            // app.UseMvc(routes =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id:int?}"
                    // defaults: new { area = "", controller = "Home", action = "Index", id = RouteParameter.Optional }
                );
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{Action=Index}/{id?}"
                    // defaults: new { area = "", controller = "Home", action = "Index", id = RouteParameter.Optional }
                );
            });
        }
    }
}