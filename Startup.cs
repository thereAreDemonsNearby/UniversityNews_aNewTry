using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using UniversityNews_aNewTry.Models;

namespace UniversityNews_aNewTry
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var dataConnection = @"Server=tcp:newsdata.database.chinacloudapi.cn,1433;Database=News;User ID=rbslt@newsdata;Password=qwer123!;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
            services.AddDbContext<NewsContext>(options => options.UseSqlServer(dataConnection));

            var identityConnection = @"Server=tcp:newsdata.database.chinacloudapi.cn,1433;Database=identitydb2;User ID=rbslt@newsdata;Password=qwer123!;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(identityConnection));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
