using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Abstraction.DAL;
using Application.Services;
using Application.Validation.Department;
using Application.Validation.Emplyee;
using AutoMapper;
using DAL.EF;
using DataAccessLayer.DAL.EF;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Departments.UI
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
            services.AddControllersWithViews();
            services.AddEntityFrameworkNpgsql()
                 .AddDbContext<EFDataContext>(options =>
                                                 options.UseNpgsql(
                                                     Configuration.GetConnectionString("PostgreDbConnectionString")));

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddScoped(typeof(IConnectionStringProvider<string>), typeof(PostgreSqlConnectionStringProvider));
            services.AddScoped(typeof(IUnitOfWorkFactory), typeof(EFUnitOfWorkFactory));
            services.AddScoped(typeof(IService<Department>), typeof(DepartmentsService));
            services.AddScoped(typeof(IService<Employee>), typeof(EmployeeService));
            services.AddScoped(typeof(AbstractValidator<Department>), typeof(DepartmentValidator));
            services.AddScoped(typeof(AbstractValidator<Employee>), typeof(EmployeeValidator));

            services.AddAutoMapper(
                typeof(Program).Assembly,
                typeof(ApplicationModule).Assembly,
                typeof(DALModule).Assembly);
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
