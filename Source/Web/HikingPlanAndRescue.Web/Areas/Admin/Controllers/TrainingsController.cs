namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Services.Data;
    using Infrastructure.CustomExceptions;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Models.Trainings;
    using Models.Users;
    using Services.Data.Contracts;
    using Web.Controllers;

    public class TrainingsController : BaseController
    {
        private ITrainingsService trainings;
        private IUsersService users;

        public TrainingsController(ITrainingsService trainings, IUsersService users)
        {
            this.trainings = trainings;
            this.users = users;
        }

        public ActionResult Index()
        {
            IQueryable<TrainingViewModel> trainings = this.trainings.GetAll().To<TrainingViewModel>();
            return this.View(trainings);
        }

        public ActionResult Edit(int id)
        {
            Training training = this.trainings.GetById(id);
            var trainingModel = this.Mapper.Map<TrainingBasicEditViewModel>(training);
            var usersModel = this.users.GetAll().To<UserEditViewModel>();
            var trainingWithUsersModel = new TrainingFullEditViewModel
            {
                Training = trainingModel,
                Users = usersModel
            };

            return this.View("Edit", trainingWithUsersModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainingFullEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.Error = "Invalid model data";
                return this.View(model);
            }

            Training training = this.trainings.GetById(model.Training.Id);
            this.Mapper.Map(model.Training, training);
            this.trainings.Save();
            this.TempData["Success"] = "Successful edit.";
            return this.RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var userId = this.User.Identity.GetUserId();
            try
            {
                this.trainings.Delete(id, userId, true);
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