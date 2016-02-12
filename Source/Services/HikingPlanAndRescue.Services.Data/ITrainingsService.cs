namespace HikingPlanAndRescue.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using HikingPlanAndRescue.Data.Models;

    public interface ITrainingsService
    {
        IQueryable<Training> GetByUser(string userId, int page = 0, int pageSize = 10);

        void AddTraining(Training training);
    }
}