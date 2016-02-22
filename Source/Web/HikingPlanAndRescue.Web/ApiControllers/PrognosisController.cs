namespace HikingPlanAndRescue.Web.ApiControllers
{
    using System.Web.Http;
    using Data.Models;
    using HikingPlanAndRescue.Web.ApiModels.Trainings;
    using Microsoft.AspNet.Identity;
    using Services.Predictions;
    using Services.Web;

    [RoutePrefix("api/prognosis")]
    [Authorize]
    public class PrognosisController : BaseApiController
    {
        private ITrainingPrediction trainingPrediction;

        public PrognosisController(ITrainingPrediction trainingPrediction)
        {
            this.trainingPrediction = trainingPrediction;
        }

        // POST: api/prognosis/training
        public IHttpActionResult PostTraining(TrainingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(new { error = "Invalid model state." });
            }

            var training = Mapper.Map<Training>(model);
            // id needed for caching
            training.UserId = User.Identity.GetUserId();
            var prognosedTraining = this.trainingPrediction.Predict(training);
            var prognosisViewModel = Mapper.Map<TrainingPrognosisViewModel>(prognosedTraining);

            return Json(prognosisViewModel);
        }
    }
}