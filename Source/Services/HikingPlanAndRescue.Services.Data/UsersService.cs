namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UsersService : IUsersService
    {
        private readonly IDbRepository<ApplicationUser> users;

        public UsersService(IDbRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.users.All();
        }
    }
}
