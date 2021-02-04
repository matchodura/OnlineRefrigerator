using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineRefrigerator.Areas.Identity.Data;
using OnlineRefrigerator.Data;

[assembly: HostingStartup(typeof(OnlineRefrigerator.Areas.Identity.IdentityHostingStartup))]
namespace OnlineRefrigerator.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));

                services.AddDefaultIdentity<AppUser>(options => {
                         options.SignIn.RequireConfirmedAccount = false;
                         options.Password.RequireLowercase = false;
                         options.Password.RequireUppercase = false;
                })

                    .AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }
}