namespace HikingPlanAndRescue.Services.Data.Contracts
{
    using System.Linq;
    using Common;
    using HikingPlanAndRescue.Data.Models;

    public interface ITrainingsService : IBaseDataWithCreatorService<Training>
    {
        Training UpdateCheckInOut(int trainingId, string watch, string userId);

        IQueryable<Training> GetCheckedIn(int page, int pageSize);
    }
}