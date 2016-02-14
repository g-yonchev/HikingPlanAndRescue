namespace HikingPlanAndRescue.Web
{
    using System;
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
        private static readonly string[] Names = new string[]
        {
            "Petkan",
            "Dragan",
            "Gosho",
            "Momchil",
            "Viara",
            "Nadejda",
            "Liubov",
            "Angel",
            "Adrian"
        };

        public static void Config()
        {
            var dbReset = false;
            if (dbReset)
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
            }
            else
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            }

            ApplicationDbContext.Create().Database.Initialize(true);

            Seed();
        }

        public static void Seed()
        {
            var context = new ApplicationDbContext();
            SeedUsers(context);
            SeedTrainings(context);
            context.SaveChanges();

            context = new ApplicationDbContext();
            SeedCheckedInTrainings(context);
            context.SaveChanges();
        }

        private static void SeedCheckedInTrainings(ApplicationDbContext context)
        {
            if (context.Trainings.Any(x => x.CheckedInOn != null && x.CheckedOutOn == null))
            {
                return;
            }

            var users = context.Users.Select(x => x.Id).ToList();
            int i = 0;
            var rand = new Random();
            foreach (var userId in users)
            {
                var futureCoef = (i % 5 == 0) ? 1 : -1;
                var randomDate = DateTime.Now.AddHours(rand.Next(1, 12) * futureCoef);
                var training = new Training()
                {
                    Title = $"Training{i}",
                    UserId = userId,
                    Calories = rand.Next(700, 3500),
                    Water = 0.5 + (rand.NextDouble() * 3),
                    StartDate = randomDate,
                    EndDate = randomDate + new TimeSpan(rand.Next(3, 12), 0, 0),
                    CheckedInOn = randomDate.AddHours(-1),
                    TrackId = i + 1
                };

                i++;
                context.Trainings.Add(training);
            }
        }

        private static void SeedTrainings(ApplicationDbContext context)
        {
            if (context.Trainings.Any())
            {
                return;
            }

            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var user = context.Users.OrderBy(x => Guid.NewGuid()).First();
                var randomDate = DateTime.Now.AddHours(rand.Next(50, 150) * (-1));
                var training = new Training()
                {
                    Title = $"Training{i}",
                    Calories = rand.Next(700, 3500),
                    Water = 0.5 + (rand.NextDouble() * 3),
                    User = user,
                    StartDate = randomDate,
                    EndDate = randomDate + new TimeSpan(rand.Next(3, 12), 0, 0),
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
            var usersCount = 50;
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
                for (int i = 1; i <= usersCount; i++)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = $"u{i}@site.com",
                        Email = $"u{i}@site.com",
                        PasswordHash = new PasswordHasher().HashPassword($"u{i}"),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        FirstName = Names[random.Next(0, Names.Length)],
                        LastName = Names[random.Next(0, Names.Length)],
                        GSM = random.Next(100000000, 999999999).ToString(),
                    };

                    context.Users.AddOrUpdate(user);
                }

                context.SaveChanges();
            }
        }
    }
}