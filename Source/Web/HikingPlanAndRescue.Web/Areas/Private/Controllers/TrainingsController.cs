namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Models;
    using Services.Data;
    using Services.Predictions;
    using Web.Controllers;

    public class TrainingsController : BaseController
    {
        private ITrainingsService trainings;
        private ITrainingPrediction trainingPredictions;
        private const int PageSize = 15;

        public TrainingsController(ITrainingsService trainings, ITrainingPrediction trainingPredictions)
        {
            this.trainings = trainings;
            this.trainingPredictions = trainingPredictions;
        }

        // GET: Private/Trainings
        public ActionResult Index(int page = 0, int pageSize = PageSize)
        {
            var trainings = this.trainings
                .GetByUser(this.User.Identity.GetUserId(), page, pageSize)
                .To<TrainingListItemViewModel>()
                .ToList();

            return this.View(trainings);
        }

        public ActionResult AjaxLoadNextTrainings(int page = 0, int pageSize = PageSize)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            Thread.Sleep(1500);

            var trainings = this.trainings
                .GetByUser(this.User.Identity.GetUserId(), page, pageSize)
                .To<TrainingListItemViewModel>()
                .ToList();

            return this.PartialView("_TrainingsList", trainings);
        }

        public ActionResult Create()
        {
            var date = DateTime.Now;
            var testTraining = new TrainingCreateViewModel()
            {
                StartDate = date,
                EndDate = date.AddHours(5),
                Title = null,
            };

            var testTrack = new TrackCreateViewModel()
            {
                Ascent = 2500,
                Title = "Test track 1",
                Length = 80,
                AscentLength = 45,
            };

            testTraining.Track = testTrack;

            return this.View(testTraining);
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
            this.trainings.AddTraining(training);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AjaxPredict(TrainingAjaxPredictViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(string.Empty);
            }

            var training = this.Mapper.Map<Training>(model);
            var predictedTraining = this.trainingPredictions.Predict(training);
            var predictedTrainingViewModel = this.Mapper.Map<TrainingAjaxPredictViewModel>(predictedTraining);

            return this.Json(predictedTrainingViewModel);
        }

        public ActionResult AjaxWatch(int trainingId, string command)
        {
            var userId = this.User.Identity.GetUserId();
            var updatedTraining = this.trainings.UpdateWatch(trainingId, command, userId);
            var updatedTrainingViewModel = this.Mapper.Map<TrainingListItemViewModel>(updatedTraining);
            return this.PartialView("_TrainingListItem", updatedTrainingViewModel);
        }
    }
}