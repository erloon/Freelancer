using System.ComponentModel.DataAnnotations;

namespace FR.IdentityServer.Model.API
{
    public class Register
    {
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string CompanyName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}