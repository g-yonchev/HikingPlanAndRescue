namespace HikingPlanAndRescue.Services.Data.Contracts
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Models;

    public interface ITrainingsService : IBaseDataService<Training>
    {
        IQueryable<Training> GetByUserWithPaging(string userId, int page = 0, int pageSize = 10);

        Training UpdateWatch(int trainingId, string watch, string userId);

        void Delete(object id, string userId, bool isAdmin);

        IQueryable<Training> GetCheckedIn(int page, int pageSize);
    }
}