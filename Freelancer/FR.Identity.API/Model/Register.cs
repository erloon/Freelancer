using System.ComponentModel.DataAnnotations;

namespace FR.Identity.API.Model
{
    public class Register
    {

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string CompanyName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}