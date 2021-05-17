using CallCentre.Interfaces.Repository;
using CallCentre.Interfaces.Services;
using CallCentre.Repository;
using CallCentre.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb
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
            services.AddDbContext<CallCentreDBContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("CC_API_SQL_CONNECTION");
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CallCentre.DataMigration.App")).UseLazyLoadingProxies();
            });

            services.AddTransient<IDataContext, CallCentreDBContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserLoginHistoryService, UserLoginHistoryService>();
            services.AddTransient<ICostingTimeSlotService, CostingTimeSlotService>();
            services.AddTransient<ICalleeService, CalleeService>();
            services.AddTransient<ICalleeCallDurationService, CalleeCallDurationService>();


            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {                
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
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
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
