using Microsoft.AspNetCore.Identity;

namespace FR.Identity.API.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
    }
}