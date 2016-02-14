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

            Thread.Sleep(1000);

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
            this.trainings.AddTraining(training);
            this.TempData["Success"] = "Item created!";

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
            var training = this.trainings.GetById(trainingId);
            var trainingModel = this.Mapper.Map<TrainingListItemViewModel>(training);
            var trainingHtml = this.RenderRazorViewToString("_TrainingListItem", trainingModel);

            Training updatedTraining = null;
            try
            {
                updatedTraining = this.trainings.UpdateWatch(trainingId, command, userId);
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
        public ActionResult Edit(TrainingEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.Error = "Invalid model data";
                return this.View(model);
            }

            Training training = this.trainings.GetById(model.Id);
            this.Mapper.Map(model, training);
            this.trainings.Update();
            this.TempData["Success"] = "Successful edit.";
            return this.RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var userId = this.User.Identity.GetUserId();
            try
            {
                this.trainings.Delete(id, userId);
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