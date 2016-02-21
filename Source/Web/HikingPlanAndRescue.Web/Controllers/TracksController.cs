namespace HikingPlanAndRescue.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using HikingPlanAndRescue.Services.Data;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using HikingPlanAndRescue.Web.ViewModels.Tracks;

    public class TracksController : BaseController
    {
        private const int PageSize = 15;
        private ITracksService tracks;

        public TracksController(ITracksService tracks)
        {
            this.tracks = tracks;
        }

        public ActionResult Index(int page = 0, int pageSize = PageSize)
        {
            var pageTracks = this.tracks
                .GetMostPopularWithPaging(page, pageSize)
                .To<TrackPublicViewModel>();

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_TracksList", pageTracks);
            }

            return this.View(pageTracks);
        }
    }
}