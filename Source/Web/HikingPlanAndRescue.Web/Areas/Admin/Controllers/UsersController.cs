namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Controllers;
    using Infrastructure.Mapping;
    using Models.Users;
    using Services.Data;

    public class UsersController : BaseController
    {
        private IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            IQueryable<UserViewModel> users = this.users.GetAll().To<UserViewModel>();
            return this.View(users);
        }
    }
}