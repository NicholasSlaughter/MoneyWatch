using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyWatch.API
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<SignInManager<ApplicationUser>>();
            services.AddTransient<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserAndHigherPolicy", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("User"))
                            || context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("Admin"))
                            || context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
                    });
                });
                options.AddPolicy("AdminAndHigherPolicy", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("Admin"))
                            || context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
                    });
                });
                options.AddPolicy("SuperAdminPolicy", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
                    });
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoneyWatch.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoneyWatch.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
