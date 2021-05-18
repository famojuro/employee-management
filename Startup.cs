using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.data;
using EmployeeManagement.Data;
using EmployeeManagement.manager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddScoped<IEmployeeManager, SqlEmployeeManager>();
            services.AddScoped<IEmployeeDataManager, SqlEmployeeDataManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // FileServerOptions fileServerOptions = new FileServerOptions();
            // fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            // fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");

            app.UseStaticFiles();

            app.UseRouting();

            app.Use(next =>  context =>
            {
                var endpoint = context.GetEndpoint();

                return next(context);
            });
            
            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello world");
            });

           // app.Use(next => context => throw new Exception("new error found"));
        }
    }
}