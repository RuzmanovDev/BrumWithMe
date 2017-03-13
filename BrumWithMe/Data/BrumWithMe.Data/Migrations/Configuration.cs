namespace BrumWithMe.Data.Migrations
{
    using Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BrumWithMeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BrumWithMeDbContext context)
        {
            context.UserTripStatus.AddOrUpdate(new UserTripStatus()
            {
                Id = 1,
                Name = "Pending"
            });

            context.UserTripStatus.AddOrUpdate(new UserTripStatus()
            {
                Id = 2,
                Name = "Accepted"
            });

            context.UserTripStatus.AddOrUpdate(new UserTripStatus()
            {
                Id = 3,
                Name = "Declined"
            });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
