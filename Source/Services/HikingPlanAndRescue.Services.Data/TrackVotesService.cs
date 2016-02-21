namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Linq;
    using Common;
    using Contracts;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public class TrackVotesService : BaseDataWithCreatorService<TrackVote>, ITrackVotesService
    {
        public TrackVotesService(IDbRepository<TrackVote> dataSet, IDbRepository<ApplicationUser> users)
            : base(dataSet, users)
        {
        }

        public void AddVote(TrackVote trackVote)
        {
            var entity = this.Data.All().FirstOrDefault(x => x.UserId == trackVote.UserId && x.TrackId == trackVote.TrackId);
            if (entity != null)
            {
                if (entity.Vote == trackVote.Vote)
                {
                    throw new CustomServiceOperationException("Cannot vote twice for the same track! You may only alter your vote.");
                }
                else if (entity.Vote != VoteType.Neutral)
                {
                    entity.Vote = VoteType.Neutral;
                }
                else
                {
                    entity.Vote = trackVote.Vote;
                }
            }
            else
            {
                this.Data.Add(trackVote);
            }

            this.Data.Save();
        }

        public int GetTrackVotesSum(int trackId)
        {
            return this.Data.All().Where(x => x.TrackId == trackId).Sum(x => (int)x.Vote);
        }
    }
}