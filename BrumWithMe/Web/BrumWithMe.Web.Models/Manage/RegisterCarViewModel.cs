using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BrumWithMe.Web.Models.Manage
{
    public class RegisterCarViewModel
    {
        [Required(ErrorMessage = "Моля, въведете цвят")]
        [MinLength(3, ErrorMessage = "Цветът трябва да е между 3 и 30 символа")]
        [MaxLength(30, ErrorMessage = "Цветът трябва да е между 3 и 30 символа")]
        public string Color { get; set; }

        [MinLength(3, ErrorMessage = "Името на марката трябва да е между 3 и 30 символа")]
        [MaxLength(30, ErrorMessage = "Името на марката трябва да е между 3 и 30 символа")]
        public string Make { get; set; }

        [MinLength(3, ErrorMessage = "Името на модела трябва да е между 3 и 30 символа")]
        [MaxLength(30, ErrorMessage = "Името на модела трябва да е между 3 и 30 символа")]
        public string Model { get; set; }

        [Range(1900, 3000, ErrorMessage = "Моля, въведете година в интервала 1900-3000")]
        public int Year { get; set; }

        public HttpPostedFileBase CarAvatar { get; set; }
    }
}
