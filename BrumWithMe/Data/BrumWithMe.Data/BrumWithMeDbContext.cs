using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BrumWithMe.Data.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using BrumWithMe.Data.Migrations;

namespace BrumWithMe.Data
{
    public class BrumWithMeDbContext : IdentityDbContext<User>
    {
        public static BrumWithMeDbContext Create()
        {
            return new BrumWithMeDbContext();
        }

        public BrumWithMeDbContext()
            : base("BrumWithMe")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BrumWithMeDbContext, Configuration>());
        }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Review> DriverReviews { get; set; }

        public virtual IDbSet<Trip> Trips { get; set; }

        public virtual IDbSet<Car> Cars { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<UsersTrips> UsersTrips { get; set; }

        public virtual IDbSet<UsersTripStatus> UserTripStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.AccessFailedCount);

            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");

            modelBuilder.Entity<User>()
                .HasMany<Review>(x => x.ReviewsByHim)
                .WithRequired(x => x.ReviewedUser)
                .HasForeignKey(x => x.ReviewedUserId);

            modelBuilder.Entity<User>()
                .HasMany<Review>(x => x.ReviewsForHim)
                .WithRequired(x => x.Creator)
                .HasForeignKey(x => x.CreatorId);

            modelBuilder.Entity<Trip>()
                .HasMany<Tag>(x => x.Tags)
                .WithMany(x => x.Trips)
                .Map(tt =>
                {
                    tt.MapLeftKey("TripId");
                    tt.MapRightKey("TagId");
                    tt.ToTable("TripTags");
                });
        }
    }
}
