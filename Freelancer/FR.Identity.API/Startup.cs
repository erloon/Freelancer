using System;
using System.Reflection;
using FR.IdentityServer.Infrastructure;
using FR.IdentityServer.Infrastructure.Database;
using FR.IdentityServer.Model;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FR.IdentityServer
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

            var connectionString = Configuration.GetConnectionString("FreelancerIdentity");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

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
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApis())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode,
        //    Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
        //    context =>
        //    {
        //        if (context.Request.Path.StartsWithSegments("/api"))
        //        {
        //            context.Response.StatusCode = (int)statusCode;
        //            return Task.CompletedTask;
        //        }
        //        return existingRedirector(context);
        //    };


        //private static void InitializeIdentityServer(IServiceProvider provider)
        //{
        //    var context = provider.GetRequiredService<ConfigurationDbContext>();
        //    if (!context.Clients.Any())
        //    {
        //        foreach (var client in IdentityServerConfiguration.GetClients())
        //        {
        //            context.Clients.Add(client.ToEntity());
        //        }
        //        context.SaveChanges();
        //    }

        //    if (!context.IdentityResources.Any())
        //    {
        //        foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
        //        {
        //            context.IdentityResources.Add(resource.ToEntity());
        //        }
        //        context.SaveChanges();
        //    }

        //    if (!context.ApiResources.Any())
        //    {
        //        foreach (var resource in IdentityServerConfiguration.GetApis())
        //        {
        //            context.ApiResources.Add(resource.ToEntity());
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }
}
