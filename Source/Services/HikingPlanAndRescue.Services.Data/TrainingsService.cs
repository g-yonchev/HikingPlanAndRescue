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
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public Training UpdateWatch(int trainingId, string command, string userId)
        {
            var training = this.trainings.All().FirstOrDefault(x => x.Id == trainingId);
            if (training == null)
            {
                return null;
            }
            else if (training.UserId != userId)
            {
                return null;
            }
            else if (command == "checkin")
            {
                training.CheckedInOn = DateTime.Now;
            }
            else if (command == "checkout")
            {
                training.CheckedOutOn = DateTime.Now;
            }
            else if (command == "reset")
            {
                training.CheckedInOn = null;
                training.CheckedOutOn = null;
            }

            this.trainings.Save();
            return training;
        }
    }
}