using CRMExample.DataAccess;
using CRMExample.DataAccess.Context;
using CRMExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupperCRMExample.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMExpample.WebApp
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
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionDB"));
            });
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(opts =>
            {
                opts.Cookie.Name = "Crm.session";
                opts.IdleTimeout = TimeSpan.FromHours(24);//Cookii 
            });

            services.AddScoped<IClientService,ClientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<INotifyRepository, NotifyRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            services.AddScoped<IClientRepository , ClientRepository>();          
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<IlogService, LogService>();
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
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Dashboard}/{id?}");
            });
        }
    }
}
