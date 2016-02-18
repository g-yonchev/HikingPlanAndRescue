namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web.Mvc;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using Infrastructure.Filters;
    using Models;
    using Services.Data.Contracts;
    using Web.Controllers;

    public class CheckedInTrainingsController : BaseController
    {
        private const int PageSize = 15;
        private ITrainingsService trainings;

        public CheckedInTrainingsController(ITrainingsService trainings)
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

        [AjaxRequestOnly]
        public ActionResult AjaxLoadNextTrainings(int page = 0, int pageSize = PageSize)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            Thread.Sleep(1000);

            var trainings = this.trainings
                .GetCheckedIn(page, pageSize)
                .To<TrainingCheckedInListItemViewModel>()
                .ToList();

            return this.PartialView("_CheckedInTrainingsList", trainings);
        }
    }
}