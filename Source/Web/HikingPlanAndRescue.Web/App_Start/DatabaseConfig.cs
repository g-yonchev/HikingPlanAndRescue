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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            ApplicationDbContext.Create().Database.Initialize(true);

            Seed();
        }

        public static void Seed()
        {
            var context = new ApplicationDbContext();
            SeedUsers(context);
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