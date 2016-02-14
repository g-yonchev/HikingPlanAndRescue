namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HikingPlanAndRescue.Services.Data;
    using Infrastructure.Mapping;
    using Models.Trainings;

    public class TrainingsController : Controller
    {
        private ITrainingsService trainings;

        public TrainingsController(ITrainingsService trainings)
        {
            this.trainings = trainings;
        }

        public ActionResult Index()
        {
            IQueryable<TrainingViewModel> trainings = this.trainings.GetAll().To<TrainingViewModel>();
            return this.View(trainings);
        }
    }
}