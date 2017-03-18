namespace BrumWithMe.Data.Models.CompositeModels.Trip
{
    public class PassangerInfo
    {
        public string Id { get; set; }

        public int TripId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public string Name
        {
            get { return $"{this.FirstName} {this.LastName}({this.UserName})"; }
        }
    }
}
