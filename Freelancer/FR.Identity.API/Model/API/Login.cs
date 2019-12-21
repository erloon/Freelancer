using System.ComponentModel.DataAnnotations;

namespace FR.IdentityServer.Model.API
{
    public class Login
    {
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}