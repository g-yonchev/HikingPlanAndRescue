namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Models;
    using Services.Data;
    using Web.Controllers;

    public class TrainingsController : BaseController
    {
        private ITrainingsService trainings;

        public TrainingsController(ITrainingsService trainings)
        {
            this.trainings = trainings;
        }

        // GET: Private/Trainings
        public ActionResult Index(int page = 0)
        {
            var trainings = this.trainings
                .GetByUser(this.User.Identity.GetUserId(), page)
                .To<TrainingListViewModel>()
                .ToList();

            return this.View(trainings);
        }

        public ActionResult AjaxLoadNextTrainings(int page = 0)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var trainings = this.trainings
                .GetByUser(this.User.Identity.GetUserId(), page)
                .To<TrainingListViewModel>()
                .ToList();

            return this.PartialView("_TrainingsList", trainings);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var training = this.Mapper.Map<Training>(model);
            training.UserId = this.User.Identity.GetUserId();

            this.trainings.AddTraining(training);
            return this.RedirectToAction("Index");
        }
    }
}