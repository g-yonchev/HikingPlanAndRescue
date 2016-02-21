namespace HikingPlanAndRescue.Web.ViewModels.TrackVotes
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using Tracks;
    using Users;

    public class TrackVoteViewModel : IMap<TrackVote>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TrackId { get; set; }

        [Required]
        [Range(-1, 1)]
        public VoteType Vote { get; set; }
    }
}