using System;
using System.Reflection;
using System.Text.Json.Serialization;
using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;
using HogwartsPotions.Services;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace HogwartsPotions
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
            services.AddDbContext<HogwartsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDefaultIdentity<Student>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HogwartsContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Student/Index";
                options.AccessDeniedPath = "/Student/AccesDenied";
                options.SlidingExpiration = true;
            });
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiForgeryField";
                options.HeaderName = "AntiForgeryHeader";
                options.Cookie.Name = "AntiForgeryCookie";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSession(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddTransient<IPotionService, PotionService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IIngredientService, IngredientService>();
            //services.AddControllers().AddJsonOptions(x =>
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' https://localhost:5001/ style-src 'unsafe-inline'  ");
            //    context.Response.Headers.Add("Strict-Transport-Security","max-age=31536000; includeSubDomains; preload ");
            //    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //    await next();
            //});
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            log4net.Config.XmlConfigurator.Configure();
            AppDomain.CurrentDomain.FirstChanceException += (s, e) =>
            {
                if (e.Exception.TargetSite.DeclaringType.Assembly == Assembly.GetExecutingAssembly())
                {
                    var exception = e.Exception;
                    ILog logger = LogManager.GetLogger("logger");
                    logger.ErrorFormat("Exception Thrown: {0}\n{1}", exception.Message, exception.StackTrace);
                }
            };
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Student}/{action=Index}/{id?}");
            });
        }
    }
}
