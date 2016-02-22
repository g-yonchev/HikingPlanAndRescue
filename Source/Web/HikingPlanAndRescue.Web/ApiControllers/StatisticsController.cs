namespace HikingPlanAndRescue.Web.ApiControllers
{
    using System.Linq;
    using System.Web.Http;
    using ApiModels.Stats;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [RoutePrefix("api/statistics")]
    public class StatisticsController : BaseApiController
    {
        private ITrainingsService db;

        public StatisticsController(ITrainingsService trainings)
        {
            this.db = trainings;
        }

        // POST: api/statistics/calories
        public IHttpActionResult PostCalories(DateRangeInputModel model)
        {
            var userId = User.Identity.GetUserId();
            var stats = db.GetAllByUser(userId).Where(x => x.StartDate >= model.StartDate && model.EndDate <= model.EndDate)
                .Select(x => new { Calories = x.Calories, Date = x.StartDate }).ToList();

            return Json(new { stats = stats });
        }
    }
}