namespace HikingPlanAndRescue.Web.Controllers
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
    using Services.Data;
    using Services.Predictions;
    using ViewModels.Trainings;
    using Web.Controllers;

    public class TrainingsController : BaseController
    {
        private ITrainingsService trainings;
        private const int PageSize = 15;

        public TrainingsController(ITrainingsService trainings)
        {
            this.trainings = trainings;
        }

        public ActionResult Index(int page = 0, int pageSize = PageSize)
        {
            var trainings = this.trainings
                .GetCheckedIn(page, pageSize)
                .To<TrainingCheckedInListItemViewModel>()
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
                .GetCheckedIn(page, pageSize)
                .To<TrainingCheckedInListItemViewModel>()
                .ToList();

            return this.PartialView("_CheckedInTrainingsList", trainings);
        }
    }
}