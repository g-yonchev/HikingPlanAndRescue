namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Linq;
    using Common;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;

    public class TracksService : BaseDataWithCreatorService<Track>, ITracksService
    {
        public TracksService(IDbRepository<Track> data, IDbRepository<ApplicationUser> users)
            : base(data, users)
        {
        }

        public IQueryable<Track> GetMostPopularWithPaging(int page, int pageSize)
        {
            return this.Data
                .All()
                .OrderByDescending(x => x.TrackVotes.Sum(tv => (int?)tv.Vote) ?? 0)
                .Skip(page * pageSize)
                .Take(pageSize);
        }
    }
}