namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public class TrainingsService : BaseDataService<Training>, ITrainingsService
    {
        private IDbRepository<ApplicationUser> users;

        public TrainingsService(IDbRepository<Training> data, IDbRepository<ApplicationUser> users)
            : base(data)
        {
            this.users = users;
        }

        public void Delete(object id, string userId, bool isAdmin)
        {
            var user = this.users.GetById(userId);
            var training = this.data.GetById(id);
            if (training == null)
            {
                throw new CustomServiceOperationException("No such training found.");
            }

            if (training.UserId != userId && !isAdmin)
            {
                throw new CustomServiceOperationException("Cannot delete trainings you do not own.");
            }

            this.data.Delete(training);
            this.data.Save();
        }

        public IQueryable<Training> GetByUserWithPaging(string userId, int page = 0, int pageSize = 10)
        {
            return this.data
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.EndDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public IQueryable<Training> GetCheckedIn(int page, int pageSize)
        {
            return this.data
                .All()
                .Where(x => x.CheckedInOn != null && x.CheckedOutOn == null)
                .OrderBy(x => x.EndDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public Training UpdateWatch(int trainingId, string command, string userId)
        {
            var training = this.data.All().FirstOrDefault(x => x.Id == trainingId);
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
                if (this.data.All()
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

            this.data.Save();
            return training;
        }
    }
}