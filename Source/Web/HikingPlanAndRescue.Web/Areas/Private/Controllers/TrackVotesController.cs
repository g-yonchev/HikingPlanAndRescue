namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.Filters;
    using HikingPlanAndRescue.Web.ViewModels.TrackVotes;
    using Infrastructure.CustomExceptions;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using Services.Data.Contracts;
    using Web.Controllers;

    public class TrackVotesController : BasePrivateController
    {
        private ITracksService tracks;
        private ITrackVotesService votes;

        public TrackVotesController(ITrackVotesService votes, ITracksService tracks)
        {
            this.votes = votes;
            this.tracks = tracks;
        }

        [AjaxRequestOnly]
        public ActionResult Vote(TrackVoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { error = "Invalid vote values!" });
            }

            var vote = Mapper.Map<TrackVote>(model);
            var userId = this.User.Identity.GetUserId();
            vote.UserId = userId;
            try
            {
                this.votes.AddVote(vote);
            }
            catch (CustomServiceOperationException e)
            {
                return this.Json(new { error = e.Message });
            }

            var totalTrackVotes = this.votes.GetTrackVotesSum(model.TrackId);

            return this.Json(new { vote = totalTrackVotes });
        }
    }
}