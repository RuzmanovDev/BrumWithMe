using System.ComponentModel.DataAnnotations;

namespace BrumWithMe.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Моля въведете валиден email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Моля въведете валиден email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Моля въведете парола!")]
        [DataType(DataType.Password, ErrorMessage = "Грешна парола!")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
