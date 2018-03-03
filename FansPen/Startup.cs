using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FansPen.Domain.Models;
using FansPen.Web.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace FansPen.Web
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
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            else
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSignalR();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddAutoMapper();
            services.AddMvc()
               .AddDataAnnotationsLocalization()
               .AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["IdFacebook"];
                facebookOptions.AppSecret = Configuration["KeyFacebook"];
            });
            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["IdTwitter"];
                twitterOptions.ConsumerSecret = Configuration["KeyTwitter"];
            });
            services.AddAuthentication().AddVK(options =>
            {
                options.ClientId = Configuration["IdVK"];
                options.ClientSecret = Configuration["KeyVK"];
                options.Scope.Add("email");
                options.Fields.Add("uid");
                options.Fields.Add("first_name");
                options.Fields.Add("last_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSignalR();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
