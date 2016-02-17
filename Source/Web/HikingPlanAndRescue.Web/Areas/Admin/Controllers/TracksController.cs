namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using HikingPlanAndRescue.Data;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Areas.Admin.Models.Tracks;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class TracksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tracks_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Track> tracks = db.Tracks;
            DataSourceResult result = tracks.ToDataSourceResult(request, c => new TrackListItemViewModel
            {
                Title = c.Title,
                Id = c.Id,
                CreatedOn = c.CreatedOn,
                ModifiedOn = c.ModifiedOn,
                Length = c.Length,
                Ascent = c.Ascent,
                AscentLength = c.AscentLength
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracks_Create([DataSourceRequest]DataSourceRequest request, TrackListItemViewModel track)
        {
            if (ModelState.IsValid)
            {
                var entity = new Track
                {
                    Title = track.Title,
                    CreatedOn = track.CreatedOn,
                    ModifiedOn = track.ModifiedOn,
                    Length = track.Length,
                    Ascent = track.Ascent,
                    AscentLength = track.AscentLength
                };

                db.Tracks.Add(entity);
                db.SaveChanges();
                track.Id = entity.Id;
            }

            return Json(new[] { track }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracks_Update([DataSourceRequest]DataSourceRequest request, TrackEditViewModel track)
        {
            if (ModelState.IsValid)
            {
                var entity = new Track
                {
                    Id = track.Id,
                    Title = track.Title,
                    CreatedOn = track.CreatedOn,
                    //ModifiedOn = track.ModifiedOn,
                    Length = track.Length,
                    Ascent = track.Ascent,
                    AscentLength = track.AscentLength
                };

                db.Tracks.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { track }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracks_Destroy([DataSourceRequest]DataSourceRequest request, TrackListItemViewModel track)
        {
            if (ModelState.IsValid)
            {
                var entity = new Track
                {
                    Id = track.Id,
                    Title = track.Title,
                    CreatedOn = track.CreatedOn,
                    ModifiedOn = track.ModifiedOn,
                    Length = track.Length,
                    Ascent = track.Ascent,
                    AscentLength = track.AscentLength
                };

                db.Tracks.Attach(entity);
                db.Tracks.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { track }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}