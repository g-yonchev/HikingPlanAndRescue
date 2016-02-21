namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using Common;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;

    public class TracksService : BaseDataWithCreatorService<Track>, ITracksService
    {
        public TracksService(IDbRepository<Track> data, IDbRepository<ApplicationUser> users)
            : base(data, users)
        {
        }
    }
}