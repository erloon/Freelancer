using System.Collections.Generic;
using FR.Identity.API.Model;

namespace FR.Identity.API.Infrastructure
{
    public static class UserRepository
    {
        public static List<AppUser> Users;
 
        static UserRepository()
        {
            Users = new List<AppUser>();
        }
    }
}