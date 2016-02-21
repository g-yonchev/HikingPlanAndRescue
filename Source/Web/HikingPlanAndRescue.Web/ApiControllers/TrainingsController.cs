namespace HikingPlanAndRescue.Web.ApiControllers
{
    using System.Net;
    using System.Web.Http;
    using ApiModels.Trainings;
    using HikingPlanAndRescue.Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [RoutePrefix("api/Trainings")]
    public class TrainingsController : BaseApiController
    {
        private ITrainingsService db;

        public TrainingsController(ITrainingsService trainings)
        {
            this.db = trainings;
        }

        // GET: api/Trainings
        public IHttpActionResult GetTrainings()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Json(new { error = "unauthorized" });
            }

            var userId = this.User.Identity.GetUserId();
            return this.Ok(this.db.GetAllByUser(userId).To<TrainingViewModel>());
        }

        // GET: api/Trainings/5
        public IHttpActionResult GetTraining(int id)
        {
            Training training = db.GetById(id);
            if (training == null)
            {
                return this.Json(new { error = "Not found." });
            }

            if (training.UserId != User.Identity.GetUserId())
            {
                return this.Json(new { error = "Cannot view training you do not own." });
            }

            return this.Ok(this.Mapper.Map<TrainingViewModel>(training));
        }

        // PUT: api/Trainings
        public IHttpActionResult PutTraining(TrainingViewModel trainingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { error = "Invalid model state." });
            }

            var training = this.db.GetById(trainingModel.Id);
            if (training == null)
            {
                return this.Json(new { error = "Training with provided Id not found." });
            }

            if (training.UserId != this.User.Identity.GetUserId())
            {
                return this.Json(new { error = "Cannot edit trainings you do not own" });
            }

            this.Mapper.Map(trainingModel, training);

            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Trainings
        public IHttpActionResult PostTraining(TrainingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(new { error = "Invalid model state." });
            }

            var training = Mapper.Map<Training>(model);
            training.UserId = User.Identity.GetUserId();

            db.Add(training);
            db.Save();

            return Ok(training.Id);
        }

        // DELETE: api/Trainings/5
        public IHttpActionResult DeleteTraining(int id)
        {
            Training training = db.GetById(id);
            if (training == null)
            {
                return this.Json(new { error = "Training not found." });
            }

            if (training.UserId != this.User.Identity.GetUserId())
            {
                return this.Json(new { error = "Cannot delete trainings you do not own" });
            }

            db.Delete(id);
            db.Save();

            return Ok("ok");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}