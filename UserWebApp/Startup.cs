using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using UserWebApp.IServices;
using UserWebApp.Models;
using UserWebApp.Services;
using UserWebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using FluentValidation;
using UserWebApp.Validators;

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
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            services.AddScoped<IBlobService, BlobService>(); 

            services.AddDbContext<UniversityContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("Database"), x => x.UseNetTopologySuite()); });

            services.AddSingleton<IUserIdProvider, CustomMessage>();

            services.AddSignalR();

            services.AddKendo();

            //services.AddSwaggerGen();

            services.AddMvc().AddFluentValidation()
            .AddMvcOptions(s => 
            {
                s.ModelBinderProviders[s.ModelBinderProviders.TakeWhile(p => !(p is ComplexTypeModelBinderProvider)).Count()] = new TrimmingModelBinderProvider();
            });

            services.AddTransient<IValidator<Streaming>, StreamingValidator>();

            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                //    options.RoutePrefix = string.Empty;
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
