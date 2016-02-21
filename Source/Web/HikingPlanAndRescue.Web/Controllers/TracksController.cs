namespace HikingPlanAndRescue.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Services.Data;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using HikingPlanAndRescue.Web.ViewModels.Tracks;
    using Infrastructure.CustomExceptions;
    using Infrastructure.Filters;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using ViewModels.TrackVotes;
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
                .GetAllWithPaging(page, pageSize)
                .To<TrackPublicViewModel>()
                .OrderBy(x => x.VotesCount);

            if (this.Request.IsAjaxRequest())
            {
                return this.PartialView("_TracksList", pageTracks);
            }

            return this.View(pageTracks);
        }
    }
}