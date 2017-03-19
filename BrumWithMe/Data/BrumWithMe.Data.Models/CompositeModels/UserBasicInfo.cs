namespace BrumWithMe.Data.Models.CompositeModels
{
    public class UserBasicInfo
    {
        public string Id { get; set; }

        public string FullName
        {
            get { return $"{this.FirstName} {this.LastName}"; }
        }
        public string UserName { get; set; }

        public double Rating { get; set; }

        public string AvataImageurl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
