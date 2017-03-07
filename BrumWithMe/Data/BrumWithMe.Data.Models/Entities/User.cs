using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BrumWithMe.Data.Models.Entities
{
    public class User : IdentityUser
    {
        private ICollection<UsersTrips> usersTrips;
        private ICollection<Car> cars;
        private ICollection<Review> reviewsForHim;
        private ICollection<Review> reviewsByHim;

        public User()
        {
            this.usersTrips = new HashSet<UsersTrips>();
            this.cars = new HashSet<Car>();
            this.reviewsForHim = new HashSet<Review>();
            this.reviewsByHim = new HashSet<Review>();
        }

        [MinLength(3)]
        [MaxLength(25)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string AvataImageurl { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{this.FirstName} {this.LastName}"; }
        }

        public virtual ICollection<UsersTrips> UsersTrips
        {
            get { return this.usersTrips; }
            set { this.usersTrips = value; }
        }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }

        public virtual ICollection<Review> ReviewsForHim
        {
            get { return this.reviewsForHim; }
            set { this.reviewsForHim = value; }
        }

        public virtual ICollection<Review> ReviewsByHim
        {
            get { return this.reviewsByHim; }
            set { this.reviewsByHim = value; }
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(this.GenerateUserIdentity(manager));
        }
    }
}
