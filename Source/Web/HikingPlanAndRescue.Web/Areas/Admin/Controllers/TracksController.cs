namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Areas.Admin.Models.Tracks;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using Web.Controllers;

    public class TracksController : BaseController
    {
        private ITracksService tracks;

        public TracksController(ITracksService tracks)
        {
            this.tracks = tracks;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Tracks_Read([DataSourceRequest]DataSourceRequest request)
        {
            var tracks = this.tracks.GetAll().To<TrackListItemViewModel>();
            var result = tracks.ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracks_Create([DataSourceRequest]DataSourceRequest request, TrackListItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.Mapper.Map<Track>(model);
                this.tracks.Add(entity);
                model.Id = entity.Id;
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracks_Update([DataSourceRequest]DataSourceRequest request, TrackEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var trackEntity = this.tracks.GetById(model.Id);
                this.Mapper.Map(model, trackEntity);
                this.tracks.Save();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tracks_Destroy([DataSourceRequest]DataSourceRequest request, TrackListItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.tracks.Delete(model.Id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            this.tracks.Dispose();
            base.Dispose(disposing);
        }
    }
}