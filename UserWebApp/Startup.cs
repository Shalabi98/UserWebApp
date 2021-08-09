using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UserWebApp.IServices;
using UserWebApp.Models;
using UserWebApp.Services;

namespace UserWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            services.AddScoped<IBlobService, BlobService>();

            services.AddDbContext<UniversityContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("Database")); });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<UniversityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Index";
            });

            var assembly = typeof(Startup).Assembly.GetName().Name;

            services.AddIdentityServer().AddAspNetIdentity<IdentityUser>().AddDeveloperSigningCredential();

            services.AddControllersWithViews();

            services.AddMvc()
            .AddMvcOptions(s =>
            {
                s.ModelBinderProviders[s.ModelBinderProviders.TakeWhile(p => !(p is ComplexTypeModelBinderProvider)).Count()] = new TrimmingModelBinderProvider();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
