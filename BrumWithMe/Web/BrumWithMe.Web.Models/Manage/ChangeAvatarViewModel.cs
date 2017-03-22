using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BrumWithMe.Web.Models.Manage
{
    public class ChangeAvatarViewModel
    {
        public string CurrentAvatarUrl { get; set; }

        [Required]
        public HttpPostedFileBase NewAvatar { get; set; }
    }
}
