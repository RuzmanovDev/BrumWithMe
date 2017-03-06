using System.Web;

namespace BrumWithMe.Web.Models.Manage
{
    public class RegisterCarViewModel
    {
        public string Color { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public HttpPostedFileBase CarAvatar { get; set; }
    }
}
