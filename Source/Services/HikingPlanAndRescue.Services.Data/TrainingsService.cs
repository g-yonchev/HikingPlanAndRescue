namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDbRepository<Training> trainings;
        private IDbRepository<ApplicationUser> users;

        public TrainingsService(IDbRepository<Training> trainings, IDbRepository<ApplicationUser> users)
        {
            this.trainings = trainings;
            this.users = users;
        }

        public void AddTraining(Training training)
        {
            this.trainings.Add(training);
            this.trainings.Save();
        }

        public void Delete(int id, string userId, bool isAdmin)
        {
            var user = this.users.GetById(userId);
            var training = this.trainings.GetById(id);
            if (training == null)
            {
                throw new CustomServiceOperationException("No such training found.");
            }

            if (training.UserId != userId && !isAdmin)
            {
                throw new CustomServiceOperationException("Cannot delete trainings you do not own.");
            }

            this.trainings.Delete(training);
            this.trainings.Save();
        }

        public IQueryable<Training> GetAll()
        {
            return this.trainings.All().OrderBy(x => x.Id);
        }

        public Training GetById(int id)
        {
            return this.trainings.GetById(id);
        }

        public IQueryable<Training> GetByUser(string userId, int page = 0, int pageSize = 10)
        {
            return this.trainings
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.EndDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public IQueryable<Training> GetCheckedIn(int page, int pageSize)
        {
            return this.trainings
                .All()
                .Where(x => x.CheckedInOn != null && x.CheckedOutOn == null)
                .OrderBy(x => x.EndDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public void Update()
        {
            this.trainings.Save();
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
                if (this.trainings.All()
                    .Any(
                    x => x.CheckedInOn != null
                    && x.CheckedOutOn == null
                    && x.UserId == userId))
                {
                    throw new CustomServiceOperationException("Cannot Check In more than one training at a time.");
                }

                training.CheckedInOn = DateTime.Now;
            }
            else if (command == "checkout")
            {
                training.CheckedOutOn = DateTime.Now;
                training.PredictedEndDate = training.EndDate;
                training.EndDate = DateTime.Now;
            }
            else if (command == "reset")
            {
                training.CheckedInOn = null;
                training.CheckedOutOn = null;
                training.EndDate = training.PredictedEndDate.Value;
            }

            this.trainings.Save();
            return training;
        }


    }
}