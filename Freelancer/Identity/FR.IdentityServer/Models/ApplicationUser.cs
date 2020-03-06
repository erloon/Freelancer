using System;
using Microsoft.AspNetCore.Identity;

namespace FR.IdentityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string PictureUrl { get; set; }
    }
}