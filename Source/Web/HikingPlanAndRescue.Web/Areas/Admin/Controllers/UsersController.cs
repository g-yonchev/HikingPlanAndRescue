namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Controllers;
    using Infrastructure.CustomExceptions;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Models.Users;
    using Services.Data;
    using Services.Data.Contracts;
    public class UsersController : BaseController
    {
        private IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        public ActionResult Index()
        {
            IQueryable<UserViewModel> users = this.users.GetAll().To<UserViewModel>();
            return this.View(users);
        }

        public ActionResult Edit(string id)
        {
            ApplicationUser user = this.users.GetById(id);
            var userModel = this.Mapper.Map<UserEditViewModel>(user);

            return this.View("Edit", userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.Error = "Invalid model data";
                return this.View(model);
            }

            ApplicationUser user = this.users.GetById(model.Id);
            this.Mapper.Map(model, user);
            this.users.Update();
            this.TempData["Success"] = "Successful edit.";
            return this.RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.Mapper.Map<ApplicationUser>(model);
            this.users.AddUser(user, model.Password);

            this.TempData["Success"] = "Item created!";

            return this.RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == this.User.Identity.GetUserId())
            {
                this.TempData["Error"] = "Cannot delete yourself";
                return this.RedirectToAction("Index");
            }

            try
            {
                this.users.Delete(id);
            }
            catch (CustomServiceOperationException e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Index");
            }

            this.TempData["Success"] = "Deleted";
            return this.RedirectToAction("Index");
        }
    }
}