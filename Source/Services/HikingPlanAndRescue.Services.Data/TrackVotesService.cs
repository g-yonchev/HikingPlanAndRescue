namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;

    public class TrackVotesService : BaseDataWithCreatorService<TrackVote>
    {
        public TrackVotesService(IDbRepository<TrackVote> dataSet, IDbRepository<ApplicationUser> users)
            : base(dataSet, users)
        {
        }
    }
}
