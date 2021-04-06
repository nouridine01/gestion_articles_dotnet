using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using outils_dotnet.Areas.Identity.Data;
using outils_dotnet.Models;

[assembly: HostingStartup(typeof(outils_dotnet.Areas.Identity.IdentityHostingStartup))]
namespace outils_dotnet.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OutilsDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OutilsDbContextConnection")));

                services.AddIdentity<User,Role>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    
                
                })
                    .AddEntityFrameworkStores<OutilsDbContext>();
            });
        }
    }
}