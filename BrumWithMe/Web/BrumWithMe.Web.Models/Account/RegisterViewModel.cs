using System.ComponentModel.DataAnnotations;

namespace BrumWithMe.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Моля въведете email!")]
        [EmailAddress(ErrorMessage = "Моля въведете валиден email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Моля въведете парола")]
        [StringLength(100, ErrorMessage = "Паролата трябва да e поне 6 символа!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Моля въведете вашето име!")]
        [MinLength(3, ErrorMessage = "Името трябва да е минимум 3 символа!")]
        [MaxLength(25, ErrorMessage = "Името трябва да е максимум 25 символа!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля въведете вашето фамилно име!")]
        [MinLength(3, ErrorMessage = "Фамилното име трябва да е минимум 3 символа!")]
        [MaxLength(25, ErrorMessage = "Фамилното име трябва да е максимум 25 символа!")]
        public string LastName { get; set; }
    }
}
