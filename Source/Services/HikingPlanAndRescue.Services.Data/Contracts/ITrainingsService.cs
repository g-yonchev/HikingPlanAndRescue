namespace HikingPlanAndRescue.Services.Data.Contracts
{
    using System;
    using System.Linq;
    using Common;
    using HikingPlanAndRescue.Data.Models;

    public interface ITrainingsService : IBaseDataWithCreatorService<Training>
    {
        Training UpdateCheckInOut(int trainingId, string watch, string userId);

        IQueryable<Training> GetCheckedIn(int page, int pageSize);

        IQueryable<Training> GetAllByUserWithPagingAndFiltering(string userId, int page, int pageSize, DateTime? fromDate, DateTime? toDate);
    }
}