using System.ComponentModel.DataAnnotations;

namespace FR.IdentityServer.Models.Identity
{
    public class RegisterRequestViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} musi mieć przynajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 2)]
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 2)]
        [Display(Name = "Nazwa firmy")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} i maksymalnie {1} znaków długości.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; } 
    }
}
