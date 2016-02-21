namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Services.Data.Common;

    public interface ITracksService : IBaseDataWithCreatorService<Track>
    {
        IQueryable<Track> GetMostPopularWithPaging(int page, int pageSize);
    }
}