namespace BrumWithMe.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Entities;
    using Models.UserRoles;
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
            this.RolesSeeder(context);

            context.UserTripStatus.AddOrUpdate(new UsersTripStatus()
            {
                Id = 1,
                Name = "Pending"
            });

            context.UserTripStatus.AddOrUpdate(new UsersTripStatus()
            {
                Id = 2,
                Name = "Accepted"
            });

            context.UserTripStatus.AddOrUpdate(new UsersTripStatus()
            {
                Id = 3,
                Name = "Owner"
            });
        }

        private void RolesSeeder(BrumWithMeDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleAdmin = new IdentityRole() { Name = UserType.Admin };

            if (!context.Roles.Any(role => role.Name == UserType.Admin))
            {
                roleManager.Create(roleAdmin);
            }
        }
    }
}
