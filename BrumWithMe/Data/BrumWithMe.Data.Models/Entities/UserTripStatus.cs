using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrumWithMe.Data.Models.Entities
{
    public class UsersTripStatus
    {
        private ICollection<UsersTrips> usersTrips;

        public UsersTripStatus()
        {
            this.usersTrips = new HashSet<UsersTrips>();
        }

        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }

        public virtual ICollection<UsersTrips> UsersTrips
        {
            get { return this.usersTrips; }

            set { this.usersTrips = value; }
        }
    }
}
