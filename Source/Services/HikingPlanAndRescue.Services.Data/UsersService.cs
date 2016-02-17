namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;
    using Microsoft.AspNet.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDbRepository<ApplicationUser> users;

        public UsersService(IDbRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        public void AddUser(ApplicationUser user, string password)
        {
            user.PasswordHash = new PasswordHasher().HashPassword(password);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = user.Email;
            this.users.Add(user);
            this.users.Save();
        }

        public void Delete(string id)
        {
            var user = this.users.GetById(id);
            if (user == null)
            {
                throw new CustomServiceOperationException("No such user id");
            }

            this.users.Delete(user);
            this.users.Save();
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.users.All().OrderBy(x => x.Id);
        }

        public ApplicationUser GetById(string id)
        {
            return this.users.GetById(id);
        }

        public void Update()
        {
            this.users.Save();
        }
    }
}