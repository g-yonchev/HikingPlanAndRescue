namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using Infrastructure.CustomExceptions;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNet.Identity;
    using Models;
    using Models.Trainings;
    using Services.Data.Contracts;
    using Services.Predictions;
    using Web.Controllers;

    public class TrainingsController : BasePrivateController
    {
        private const int PageSize = 15;
        private ITrainingsService trainings;
        private ITrainingPrediction trainingPredictions;

        public TrainingsController(ITrainingsService trainings, ITrainingPrediction trainingPredictions)
        {
            this.trainings = trainings;
            this.trainingPredictions = trainingPredictions;
        }

        public ActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            var userId = this.User.Identity.GetUserId();
            var foundTrainings = this.trainings
                .GetAllByUserWithPagingAndFiltering(userId, 0, PageSize, fromDate, toDate);
            var trainings = foundTrainings
                .To<TrainingListItemViewModel>()
                .ToList();

            var stats = new TrainingsStatsViewModel()
            {
                TotalAscent = (foundTrainings.Count() > 0) ? foundTrainings.Sum(x => x.Track.Ascent) : 0,
                TotalLength = (foundTrainings.Count() > 0) ? foundTrainings.Sum(x => x.Track.Length) : 0,
                TotalCalories = (foundTrainings.Count() > 0) ? foundTrainings.Sum(x => x.Calories) : 0,
            };

            var model = new TrainingsIndexViewModel()
            {
                Stats = stats,
                Trainings = trainings
            };

            return this.View(model);
        }

        [AjaxRequestOnly]
        public ActionResult AjaxLoadNextTrainings(DateTime? fromDate, DateTime? toDate, int page = 0, int pageSize = PageSize)
        {
            var userId = this.User.Identity.GetUserId();
            Thread.Sleep(1000);
            var trainings = this.trainings
                .GetAllByUserWithPagingAndFiltering(userId, page, pageSize, fromDate, toDate)
                .To<TrainingListItemViewModel>()
                .ToList();

            return this.PartialView("_TrainingsList", trainings);
        }

        public ActionResult Create()
        {
            var date = DateTime.Now;
            var testTraining = new TrainingCreateViewModel()
            {
                Title = "Test Training 1",
                StartDate = date,
                EndDate = date.AddHours(5),
            };

            var testTrack = new TrackCreateViewModel()
            {
                Title = "Test track 1",
                Ascent = 2500,
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
            this.trainings.Add(training);
            this.TempData["Success"] = "Item created!";

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [AjaxRequestOnly]
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

        [AjaxRequestOnly]
        public ActionResult AjaxWatch(int trainingId, string command)
        {
            var userId = this.User.Identity.GetUserId();
            var training = this.trainings.GetById(trainingId);
            var trainingModel = this.Mapper.Map<TrainingListItemViewModel>(training);
            var trainingHtml = this.RenderRazorViewToString("_TrainingListItem", trainingModel);

            Training updatedTraining = null;
            try
            {
                updatedTraining = this.trainings.UpdateCheckInOut(trainingId, command, userId);
            }
            catch (CustomServiceOperationException e)
            {
                return this.Json(
                    new
                    {
                        error = e.Message,
                        data = trainingHtml,
                        id = training.Id
                    }, JsonRequestBehavior.AllowGet);
            }

            var updatedTrainingViewModel = this.Mapper.Map<TrainingListItemViewModel>(updatedTraining);
            trainingHtml = this.RenderRazorViewToString("_TrainingListItem", updatedTrainingViewModel);
            return this.Json(
                    new
                    {
                        data = trainingHtml,
                        id = training.Id
                    }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            Training training = this.trainings.GetById(id);
            var trainingModel = this.Mapper.Map<TrainingEditViewModel>(training);

            return this.View("Edit", trainingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainingEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.Error = "Invalid model data";
                return this.View(model);
            }

            Training training = this.trainings.GetById(model.Id);
            this.Mapper.Map(model, training);
            this.trainings.Save();
            this.TempData["Success"] = "Successful edit.";
            return this.RedirectToAction("Index");
        }

        [AjaxRequestOnly]
        public ActionResult Delete(int id)
        {
            var userId = this.User.Identity.GetUserId();
            try
            {
                this.trainings.Delete(id, userId, false);
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