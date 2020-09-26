using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AddressBookIdentity.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AddressBookIdentity
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ContactDbContext>(options => options.UseSqlServer(connection));
            services.AddIdentity<IdentityUser, IdentityRole>()
                     .AddEntityFrameworkStores<ContactDbContext>();
            services.AddControllersWithViews();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{

            //      app.UseDeveloperExceptionPage();

            //}
            //else
            //{
            //      app.UseStatusCodePagesWithRedirects("/Error/{0}");
            //}

            //Centralized exception handling
            app.UseStatusCodePagesWithRedirects("/Error/{1}");
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=AddContact}/{id?}");
            });
        }
    }
}
