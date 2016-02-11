namespace HikingPlanAndRescue.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using Data.Migrations;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class DatabaseConfig
    {
        public static void Config()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            ApplicationDbContext.Create().Database.Initialize(true);

            Seed();
        }

        public static void Seed()
        {
            var context = new ApplicationDbContext();
            SeedUsers(context);
            SeedTrainings(context);
            context.SaveChanges();
        }

        private static void SeedTrainings(ApplicationDbContext context)
        {
            if (context.Trainings.Any())
            {
                return;
            }

            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                var user = context.Users.OrderBy(x => Guid.NewGuid()).First();
                var training = new Training()
                {
                    Calories = rand.Next(700, 3500),
                    Water = 0.5 + (rand.NextDouble() * 3),
                    User = user,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow + new TimeSpan(rand.Next(3, 12), 0, 0),
                };

                var ascentLen = 5 + (rand.NextDouble() * 30);

                var track = new Track()
                {
                    AscentLength = ascentLen,
                    Ascent = ascentLen * rand.Next(20, 100),
                    Length = ascentLen * (1.5 + (rand.NextDouble() * 2.5)),
                    Title = $"Track{i}",
                    User = user
                };

                training.Track = track;
                context.Trainings.Add(training);
            }
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@site.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new ApplicationUserManager(store);
                var user = new ApplicationUser
                {
                    Email = "admin@site.com",
                    UserName = "admin@site.com",
                    PasswordHash = new PasswordHasher().HashPassword("admin"),
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                context.Users.AddOrUpdate(user);
                context.SaveChanges();
                manager.AddToRole(user.Id, "Admin");
            }

            if (context.Users.Count() <= 1)
            {
                var random = new Random();
                for (int i = 1; i <= 5; i++)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = $"u{i}@site.com",
                        Email = $"u{i}@site.com",
                        PasswordHash = new PasswordHasher().HashPassword($"u{i}"),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    context.Users.AddOrUpdate(user);
                }

                context.SaveChanges();
            }
        }
    }
}