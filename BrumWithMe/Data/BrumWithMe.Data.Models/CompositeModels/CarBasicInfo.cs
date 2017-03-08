namespace BrumWithMe.Data.Models.CompositeModels
{
    public class CarBasicInfo
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public string Color { get; set; }

        public override string ToString()
        {
            return $"{this.Make} {this.Model} ({this.Year})";
        }
    }
}
