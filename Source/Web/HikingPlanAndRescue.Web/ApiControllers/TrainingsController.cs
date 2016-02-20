namespace HikingPlanAndRescue.Web.ApiControllers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;
    using ApiModels.Trainings;
    using HikingPlanAndRescue.Data;
    using HikingPlanAndRescue.Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;

    [RoutePrefix("api/Trainings")]
    public class TrainingsController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Trainings
        public IHttpActionResult GetTrainings()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Json(new { error = "unauthorized" });
            }

            var userId = this.User.Identity.GetUserId();
            return this.Ok(this.db.Trainings.Where(x => x.UserId == userId).To<TrainingViewModel>());
        }

        // GET: api/Trainings/5
        public IHttpActionResult GetTraining(int id)
        {
            Training training = db.Trainings.Find(id);
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

            var training = this.db.Trainings.FirstOrDefault(x => x.Id == trainingModel.Id);
            if (training == null)
            {
                return this.Json(new { error = "Training with provided Id not found." });
            }

            if (training.UserId != this.User.Identity.GetUserId())
            {
                return this.Json(new { error = "Cannot edit trainings you do not own" });
            }

            this.Mapper.Map(trainingModel, training);

            this.db.Entry(training).State = EntityState.Modified;

            db.SaveChanges();

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

            db.Trainings.Add(training);
            db.SaveChanges();

            return Ok(training.Id);
        }

        // DELETE: api/Trainings/5
        public IHttpActionResult DeleteTraining(int id)
        {
            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return this.Json(new { error = "Training not found." });
            }

            if (training.UserId != this.User.Identity.GetUserId())
            {
                return this.Json(new { error = "Cannot delete trainings you do not own" });
            }

            db.Trainings.Remove(training);
            db.SaveChanges();

            return Ok(training);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainingExists(int id)
        {
            return db.Trainings.Count(e => e.Id == id) > 0;
        }
    }
}