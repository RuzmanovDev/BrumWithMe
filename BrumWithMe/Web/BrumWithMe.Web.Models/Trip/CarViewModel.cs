namespace BrumWithMe.Web.Models.Trip
{
    public class CarViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        public string ImageUrl { get; set; }

        public string Name
        {
            get
            {
                return $"{this.Make} {this.Model} ({this.Year})";
            }
        }
    }
}
