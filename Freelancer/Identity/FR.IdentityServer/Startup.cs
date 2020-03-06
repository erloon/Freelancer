using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FR.IdentityServer.Infrastructure;
using FR.IdentityServer.Infrastructure.Database;
using FR.IdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FR.IdentityServer
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
             services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("FreelancerIdentity"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = o =>
                    {
                        o.UseSqlServer(Configuration.GetConnectionString("FreelancerIdentity"), so =>
                        {
                            so.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name);
                        });

                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = o =>
                    {
                        o.UseSqlServer(Configuration.GetConnectionString("FreelancerIdentity"), so =>
                            {
                                so.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name);
                            });

                    };

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<ApplicationUser>();



            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Contains("Development"))
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddTransient<IProfileService, IdentityClaimsProfileService>();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
            services.AddControllers();

            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
