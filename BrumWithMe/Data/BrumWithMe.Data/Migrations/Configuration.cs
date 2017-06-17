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
    using System.Collections.Generic;
    using System.IO;

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
            this.UsersSeeder(context);
            this.UserTripStatusSeeder(context);
            this.TagsSeeder(context);
            this.CitySeeder(context);

            //context.SaveChanges();
        }

        private void CitySeeder(BrumWithMeDbContext context)
        {
            if (!context.Cities.Any(x => x.Name == "София"))
            {
                context.Cities.AddOrUpdate(new City() { Name = "София" });
            }

            if (!context.Cities.Any(x => x.Name == "Велико Търново"))
            {
                context.Cities.AddOrUpdate(new City() { Name = "Велико Търново" });
            }

            if (!context.Cities.Any(x => x.Name == "Пловдив"))
            {
                context.Cities.AddOrUpdate(new City() { Name = "Пловдив" });
            }

            if (!context.Cities.Any(x => x.Name == "Пазарджик"))
            {
                context.Cities.AddOrUpdate(new City() { Name = "Пазарджик" });
            }

            if (!context.Cities.Any(x => x.Name == "Бургас"))
            {
                context.Cities.AddOrUpdate(new City() { Name = "Бургас" });
            }
        }

        private void TagsSeeder(BrumWithMeDbContext context)
        {
            Tag noSmoking = new Tag() { Name = "Забранено пушенето" };
            Tag noPets = new Tag() { Name = "Без домашни любимци" };
            Tag bigTrunk = new Tag() { Name = "Голям багажник" };
            Tag withoudLuggage = new Tag() { Name = "Без багаж" };
            Tag noStops = new Tag() { Name = "Без спирания" };

            if (!context.Tags.Any(x => x.Name == "Забранено пушенето"))
            {
                context.Tags.AddOrUpdate(noSmoking);
            }

            if (!context.Tags.Any(x => x.Name == "Без домашни любимци"))
            {
                context.Tags.AddOrUpdate(noPets);
            }

            if (!context.Tags.Any(x => x.Name == "Голям багажник"))
            {
                context.Tags.AddOrUpdate(bigTrunk);
            }

            if (!context.Tags.Any(x => x.Name == "Без багаж"))
            {
                context.Tags.AddOrUpdate(withoudLuggage);
            }

            if (!context.Tags.Any(x => x.Name == "Без спирания"))
            {
                context.Tags.AddOrUpdate(noStops);
            }
        }

        private void UsersSeeder(BrumWithMeDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@test.test"))
            {
                var adminUser = new User
                {
                    UserName = "admin@test.test",
                    Email = "admin@test.test",
                    AvatarImageurl = "/UserAvatars/default.png",
                    FirstName = "Стоян",
                    LastName = "Рузманов"
                };

                userManager.Create(adminUser, "admin@2");
                userManager.AddToRole(adminUser.Id, UserType.Admin);
            }

            if (!context.Users.Any(u => u.UserName == "user@test.test"))
            {
                var regularUser = new User
                {
                    UserName = "user@test.test",
                    Email = "user@test.test",
                    AvatarImageurl = "/UserAvatars/default.png",
                    FirstName = "Пешо",
                    LastName = "Пешев"
                };

                userManager.Create(regularUser, "admin@2");
            }
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

        private void UserTripStatusSeeder(BrumWithMeDbContext context)
        {
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
    }
}
