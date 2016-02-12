namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDbRepository<Training> trainings;

        public TrainingsService(IDbRepository<Training> trainings)
        {
            this.trainings = trainings;
        }

        public void AddTraining(Training training)
        {
            this.trainings.Add(training);
            this.trainings.Save();
        }

        public IQueryable<Training> GetByUser(string userId, int page = 0, int pageSize = 10)
        {
            return this.trainings
                .All()
                .Where(x => x.UserId == userId).OrderBy(x => x.Id)
                .Skip(page * pageSize)
                .Take(pageSize);
        }
    }
}
