using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.PL.MappingProfiles;
using demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;

namespace Demo.PL
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
            services.AddDbContext<MvcAppDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }); //allow dependancy injection               (.... , scope aw transient aw singleton lw 3ayz a8yr eldefault l hwa scoped)
            services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //allow dependancy injection
            services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //el object mwgod tol ftrt elrequest bs wlma ttqfl wtft7 hycreate obj tany
            //services.AddTransient<IEmployeeRepository, EmployeeRepository>();//el object hy3esh l7d ma el operation t5ls
            //services.AddSingleton<IEmployeeRepository, EmployeeRepository>();//lifetime el ojbect felheap hyfdl tol ftrt elruntime wel app mfto7
            services.AddAutoMapper(M => M.AddProfiles(new List<Profile>() { new EmployeeProfile(),new UserProfile(),new RoleProfile()}));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true; //@ #
                Options.Password.RequireDigit = true; //12
                Options.Password.RequireLowercase = true;
            })
                .AddEntityFrameworkStores<MvcAppDbContext>()
                .AddDefaultTokenProviders(); //ht3ml token ll login,reset pw,2 factor auth
            //services.AddScoped<UserManager<ApplicationUser>>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//5las msh m7tag t add service llsign in /user/role managers kolhom
                .AddCookie(Options =>
                {
                    Options.LoginPath ="Account/Login";
                    Options.AccessDeniedPath = "Home/Error";
                }); 
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
            app.UseHttpsRedirection(); //redirect url to https
            app.UseStaticFiles();

            app.UseRouting();  //trteb elmiddleware byfr2
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}

//P@ssw0rd //bnst5dmo ka test elpw
//Pa$$w0rd