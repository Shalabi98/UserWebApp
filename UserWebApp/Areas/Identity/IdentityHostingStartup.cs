using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserWebApp.Areas.Identity.Data;
using UserWebApp.Data;

[assembly: HostingStartup(typeof(UserWebApp.Areas.Identity.IdentityHostingStartup))]
namespace UserWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;

                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<AuthDbContext>();

                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.Name = "Identity.Cookie";
                    //options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
                    //options.SlidingExpiration = true;
                });
            });
        }
    }
}