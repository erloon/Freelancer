using System.ComponentModel.DataAnnotations;

namespace FR.Identity.API.Model
{
    public class Login
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}