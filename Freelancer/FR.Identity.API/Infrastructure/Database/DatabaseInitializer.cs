using System;
using System.Linq;
using System.Security.Claims;
using FR.IdentityServer.Model;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FR.IdentityServer.Infrastructure.Database
{
    public class DatabaseInitializer
    {
        public static void Init(IServiceProvider provider, bool useInMemoryStores)
        {
            if (!useInMemoryStores)
            {
                provider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                provider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                provider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            }
            InitializeIdentityServer(provider);

            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
            var erloon = userManager.FindByNameAsync("erloon").Result;
            if (erloon == null)
            {
                erloon = new ApplicationUser
                {
                    Email = "erloon@wp.pl",
                    UserName = "erloon",
                    CompanyName = "sparkdata.pl"
                };
                var result = userManager.CreateAsync(erloon, "$AspNetIdentity10$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                erloon = userManager.FindByNameAsync("erloon").Result;

                result = userManager.AddClaimsAsync(erloon, new Claim[]{
                    new Claim(JwtClaimTypes.Name, "Maciej Kryca"),
                    new Claim(JwtClaimTypes.GivenName, "Maciej"),
                    new Claim(JwtClaimTypes.FamilyName, "Kryca"),
                    new Claim(JwtClaimTypes.Email, "erloon@wp.pl"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "https://sparkdata.pl"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'localhost 10', 'postal_code': 11146, 'country': 'poland' }", 
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Console.WriteLine("erloon created");
            }
            else
            {
                Console.WriteLine("erloon already exists");
            }
        }

        private static void InitializeIdentityServer(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ConfigurationDbContext>();
            if (!context.Clients.Any())
            {
                foreach (var client in IdentityServerConfiguration.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetApis())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}